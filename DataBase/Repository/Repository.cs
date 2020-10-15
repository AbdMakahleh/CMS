using DataBase.Context;
using DataBase.Entities;
using Infrastructure.ApiResponse;
using Infrastructure.Encryption;
using Infrastructure.Entity;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;

namespace DataBase.Repository
{
    public class Repository<T> : IRepository<T> where T : class, IEntity, new()
    {
        private DbSet<T> _entities;
        private string _errorMessage = string.Empty;
        private bool _isDisposed;
        private readonly ILogger<T> _logger;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork<CMSContext> _unitOfWork;
        public Repository(IUnitOfWork<CMSContext> unitOfWork, ILogger<T> logger,IConfiguration configuration): this(unitOfWork.Context)
        {
            _logger = logger;
            _isDisposed = false;
            _config = configuration;
            _unitOfWork = unitOfWork;
        }


        public Repository(CMSContext context)
        {
            _isDisposed = false;
            Context = context;
        }
        private CMSContext OpenConnection(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CMSContext>();
            optionsBuilder.UseNpgsql(Encryption.Instance.Decrypt(connectionString));
            return new CMSContext(optionsBuilder.Options);
        }
        public CMSContext Context { get; set; }
        public virtual IQueryable<T> Table
        {
            get { return Entities; }
        }
        protected virtual DbSet<T> Entities => _entities ?? (_entities = Context.Set<T>());
        public void Dispose()
        {
            if (Context != null)
                Context.Dispose();
            _isDisposed = true;
        }
        public virtual IResponseResult GetAll()
        {
            // log select statement 
            return new ResponseResult<IEnumerable<T>>
            {
                Code = HttpStatusCode.OK,
                Data = Entities.ToList(),
                Status = true,
                Message = "Success",
                ErrorMessage = string.Empty

            };
        }
        public virtual IResponseResult GetById(object id)
        {

            // log select statement 
            return new ResponseResult<T>()
            {
                Message = "",
                Status = true,
                Data = Entities.Find(id),
                Code = HttpStatusCode.OK
            };
        }
        public virtual IResponseResult Insert(T t) 
        {

            // log insert 
            _unitOfWork.CreateTransaction();
            Entity entity = (Entity)(object)t;
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = "System";
            // in case there are logged in user set created user ID;
            // else set System
            if (entity == null)
                throw new ArgumentNullException("entity");
            Entities.Add((T)(object)entity);
            if (Context == null || _isDisposed)
                Context = new CMSContext();
            if (_unitOfWork.Save())
            {
                _unitOfWork.Commit();
                return new ResponseResult<T>
                {
                    Data = (T)(object)entity,
                    ErrorMessage = string.Empty,
                    Message = "Successfully inserted",
                    Code = HttpStatusCode.OK,
                    Status = true
                };
           
            } else
            {
                return new ResponseResult
                {

                    Message = string.Empty,
                    ErrorMessage = "Insertion Failed",
                    Code = HttpStatusCode.OK,
                    Status= false
                };
            }
           
         
           

        }
  
public T Format<T>(object obj) => JsonConvert.DeserializeObject<T>(obj.ToString());
    public void BulkInsert(IEnumerable<T> entities)
        {
            // log bulk insert
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            Context.Set<T>().AddRange(entities);


        }
        public virtual void Update(T entity)
        {
            // log update

            if (entity == null)
                throw new ArgumentNullException("entity");
            if (Context == null || _isDisposed)
                Context = new CMSContext();
            SetEntryModified(entity);
            //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work

        }
        public virtual void Delete(T entity)
        {
            // log update
            try
            {
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new CMSContext();
                SetEntryModified(entity);
            }
            catch (Exception ex)
            {
                var x = 1;
            }

            //Context.SaveChanges(); commented out call to SaveChanges as Context save changes will be called with Unit of work

        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            IQueryable<T> dbQuery = Context.Set<T>();

            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
            {
                dbQuery = dbQuery.AsNoTracking().Include<T, object>(navigationProperty);
            }

            return dbQuery.Where(predicate);

        }

        public virtual IQueryable<T> Find(Expression<Func<T, bool>> predicate)
        {
            var query = Context.Set<T>().Where(predicate);
            return query;
        }
        public virtual void SetEntryModified(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        public bool SaveChanges()
        {
            return Context.SaveChanges() > -1;
        }
    }

}
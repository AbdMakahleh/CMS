using DataBase.Context;
using DataBase.Entities;
using DataBase.Models;
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
        private readonly ILogger<Log> _logger;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork<CMSContext> _unitOfWork;
        public Repository(IUnitOfWork<CMSContext> unitOfWork, ILogger<Log> logger,IConfiguration configuration): this(unitOfWork.Context)
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
            return new ResponseResult<T>()
            {
                Message = "Success",
                Status = true,
                Data = Entities.Find(id),
                Code = HttpStatusCode.OK
            };
        }
        public virtual IResponseResult Insert(T entity) 
        {
            // log insert 
            _unitOfWork.CreateTransaction();
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = "System";
            // in case there are logged in user set created user ID;
            // else set System
            if (entity == null)
                throw new ArgumentNullException("entity");
            Entities.Add(entity);
            if (Context == null || _isDisposed)
                Context = new CMSContext();
    
                if(_unitOfWork.Commit())
                return new ResponseResult<T>
                {
                    Data = (T)(object)entity,
                    ErrorMessage = string.Empty,
                    Message = "Successfully inserted",
                    Code = HttpStatusCode.OK,
                    Status = true
                };
                return new ResponseResult
                {

                    Message = string.Empty,
                    ErrorMessage = "Insertion Failed",
                    Code = HttpStatusCode.OK,
                    Status = false
                };
        }
  
public T Format<T>(object obj) => JsonConvert.DeserializeObject<T>(obj.ToString());
    public IResponseResult BulkInsert(IEnumerable<T> entities)
        {
            // log bulk insert
            _unitOfWork.CreateTransaction();
            if (entities == null)
            {
                throw new ArgumentNullException("entities");
            }
            Context.ChangeTracker.AutoDetectChangesEnabled = false;
            Context.Set<T>().AddRange(entities);
            if (_unitOfWork.Commit())
                return new ResponseResult<List<T>>
                {
                    Data = entities.ToList(),
                    Status = true,
                    Code = HttpStatusCode.OK,
                    ErrorMessage = string.Empty,
                    Message = "Inserted Successfully"
                };
            return new ResponseResult
            {
   
                Status = true,
                Code = HttpStatusCode.OK,
                ErrorMessage = string.Empty,
                Message = "Insertion Failed"
            };

        }
        public virtual IResponseResult Update(T entity)
        {
            // log update
            _unitOfWork.CreateTransaction();
  
            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = "System";
            if (entity == null)
                throw new ArgumentNullException("entity");
            if (Context == null || _isDisposed)
                Context = new CMSContext();
            SetEntryModified(entity);
            if (_unitOfWork.Commit())
            {
                return new ResponseResult<T>
                {
                    Data = entity,
                    Status = true,
                    Code = HttpStatusCode.OK,
                    ErrorMessage = string.Empty,
                    Message = "Successfully Updated"
                };
            } else
            {
                return new ResponseResult
                {
                    Status = true,
                    Code = HttpStatusCode.OK,
                    ErrorMessage = "Field to  Update",
                    Message = string.Empty
                };
            }
        }
        public IResponseResult UpdateEntities(List<T> entities)
        {
            int count = entities.Count();
            for (int i = 0; i < count; i++)
            {
                var enitity = (Entity)(object)entities[i];
                enitity.UpdatedBy = enitity.UpdatedBy;//((UserIdentity)Thread.CurrentPrincipal?.Identity)?.ID.ToString() ?? "System";
                enitity.UpdatedAt = DateTime.Now;
                Context.Attach(enitity);
                Context.Entry(enitity).State = EntityState.Modified;
                if (_unitOfWork.Commit())
                    continue;
                else
                    return new ResponseResult<List<T>>
                    {
                        Code = HttpStatusCode.OK,
                        Data = entities,
                        Status = true,
                        ErrorMessage = "Updating failed",
                        Message = string.Empty
                    };

            }
            return new ResponseResult<List<T>>
            {
                Code = HttpStatusCode.OK,
                Data = entities,
                Status = true,
                ErrorMessage = string.Empty,
                Message = "Success"
            };
         
        }

        public virtual IResponseResult Delete(T entity)
        {
                _unitOfWork.CreateTransaction();
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                entity.UpdatedBy = "System";
                if (entity == null)
                    throw new ArgumentNullException("entity");
                if (Context == null || _isDisposed)
                    Context = new CMSContext();
                SetEntryModified(entity);
            if (_unitOfWork.Commit())
            {
                return new ResponseResult
                {
                    Code = HttpStatusCode.OK,
                    Status = true,
                    ErrorMessage = string.Empty,
                    Message = "Deleted Successfully"
                };
            } else
            {
                return new ResponseResult
                {
                    Code = HttpStatusCode.OK,
                    Status = false,
                    ErrorMessage = string.Empty,
                    Message = "Deleted failed"
                };
            }
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
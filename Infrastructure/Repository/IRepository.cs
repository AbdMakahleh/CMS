using Infrastructure.ApiResponse;
using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Repository
{
    public interface IRepository<T> where T : class,  IEntity, new()
    {
        IResponseResult GetAll();
        IResponseResult GetById(object id);
        IResponseResult Insert(T obj);
        IResponseResult Update(T obj);
        IResponseResult Delete(T obj);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties);
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        bool SaveChanges();
        IResponseResult BulkInsert(IEnumerable<T> entities);
        IResponseResult UpdateEntities(List<T> entities);
    }
}

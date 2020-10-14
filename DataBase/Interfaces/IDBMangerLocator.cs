using DataBase.Repository;
using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Interfaces
{
   public interface IDBMangerLocator<Entity> where Entity : class, IEntity, new()
    {
        public Lazy<RespositoryUnitOfWork<Entity>> RespositoryUnitOfWork { get; set; }
    }
}

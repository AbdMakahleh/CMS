using DataBase.Context;
using DataBase.Interfaces;
using DataBase.Repository;
using Infrastructure.Entity;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Locater
{
    public class DBMangerLocator<T> : IDBMangerLocator<T> where T : class, IEntity, new()
    {


        public Lazy<RespositoryUnitOfWork<T>> RespositoryUnitOfWork { get; set; }
        public DBMangerLocator(IUnitOfWork<CMSContext> unitOfWork, ILogger<T> logger, IConfiguration configuration)
        {
           
            RespositoryUnitOfWork = new Lazy<RespositoryUnitOfWork<T>>(() => new RespositoryUnitOfWork<T>(unitOfWork, logger, configuration));
        }


    }
}

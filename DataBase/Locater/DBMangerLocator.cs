using DataBase.Context;
using DataBase.DBManagers;
using DataBase.Interfaces;
using DataBase.Models;
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
    public class DBMangerLocator : IDBMangerLocator
    {


        public Lazy<UserDBManager> User { get; set; }
        public DBMangerLocator(IUnitOfWork<CMSContext> unitOfWork, ILogger<Log> logger, IConfiguration configuration)
        {

            User = new Lazy<UserDBManager>(() => new UserDBManager(unitOfWork, logger, configuration));
        }


    }
}

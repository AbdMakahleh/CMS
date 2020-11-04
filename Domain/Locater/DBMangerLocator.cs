using DataBase.Context;
using DataBase.Interfaces;
using DataBase.Models;
using Domain.DBManagers;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;


namespace Domain.Locater
{
    public class DBMangerLocator : IDBMangerLocator
    {


        public Lazy<UserDBManager> User { get; set; } 
        public Lazy<CMSModuleDBManager> CMSModule { get; set; }
        public Lazy<LookupDBManager> Lookup { get; set; }
        public DBMangerLocator(IUnitOfWork<CMSContext> unitOfWork, ILogger<Log> logger, IConfiguration configuration)
        {

            User = new Lazy<UserDBManager>(() => new UserDBManager(unitOfWork, logger, configuration));
            CMSModule = new Lazy<CMSModuleDBManager>(() => new CMSModuleDBManager(unitOfWork, logger, configuration));
            Lookup = new Lazy<LookupDBManager>(() => new LookupDBManager(unitOfWork, logger, configuration));
        }


    }
}

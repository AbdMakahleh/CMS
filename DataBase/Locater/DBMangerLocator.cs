using DataBase.Context;
using DataBase.Interfaces;
using DataBase.Models;
using DataBase.DBManagers;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;


namespace DataBase.Locater
{
    public class DBMangerLocator : IDBMangerLocator
    {


        public Lazy<UserDBManager> User { get; set; }
        public Lazy<RoleDBManager> Role { get; set; }
        public Lazy<CMSModuleDBManager> CMSModule { get; set; }
        public Lazy<LookupDBManager> Lookup { get; set; }
        public DBMangerLocator(IUnitOfWork<CMSContext> unitOfWork, ILogger<Log> logger, IConfiguration configuration)
        {
            User = new Lazy<UserDBManager>(() => new UserDBManager(unitOfWork, logger, configuration));
            Role = new Lazy<RoleDBManager>(() => new RoleDBManager(unitOfWork, logger, configuration));
            CMSModule = new Lazy<CMSModuleDBManager>(() => new CMSModuleDBManager(unitOfWork, logger, configuration));
            Lookup = new Lazy<LookupDBManager>(() => new LookupDBManager(unitOfWork, logger, configuration));
        }


    }
}

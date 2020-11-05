using DataBase.Context;
using DataBase.Models;
using DataBase.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DBManagers
{
  public class LookupDBManager 
    {
        public Lazy<Repository<Lookup>> Repository;
        public LookupDBManager(IUnitOfWork<CMSContext> unitOfWork,ILogger<Log> logger,IConfiguration configuration)
        {
            Repository = new Lazy<Repository<Lookup>>(() => new Repository<Lookup>(unitOfWork, logger, configuration));
        }


    
    }
}

using DataBase.Context;
using DataBase.Models;
using DataBase.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.DBManagers
{
  public class UserDBManager 
    {
        public Lazy<Repository<User>> Repository;
        public UserDBManager(IUnitOfWork<CMSContext> unitOfWork,ILogger<Log> logger,IConfiguration configuration)
        {
            Repository = new Lazy<Repository<User>>(() => new Repository<User>(unitOfWork, logger, configuration));
        }


    
    }
}

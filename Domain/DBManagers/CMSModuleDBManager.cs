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
  public class CMSModuleDBManager 
    {
        public Lazy<Repository<Cmsmodule>> Repository;
        public CMSModuleDBManager(IUnitOfWork<CMSContext> unitOfWork,ILogger<Log> logger,IConfiguration configuration)
        {
            Repository = new Lazy<Repository<Cmsmodule>>(() => new Repository<Cmsmodule>(unitOfWork, logger, configuration));
        }


    
    }
}

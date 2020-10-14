using DataBase.Context;
using Infrastructure.Entity;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBase.Repository
{
    public class RespositoryUnitOfWork<Entity> where Entity : class, IEntity, new()
    {

        public Lazy<Repository<Entity>> _repo;
        public RespositoryUnitOfWork(IUnitOfWork<CMSContext> unitOfWork, ILogger<Entity> logger, IConfiguration configuration)
        {
            _repo = new Lazy<Repository<Entity>>(() => new Repository<Entity>(unitOfWork, logger, configuration));
        }
    }
}

using DataBase.Context;
using DataBase.Models;
using DataBase.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataBase.DBManagers
{
    public class RoleDBManager
    {
        public Lazy<Repository<Role>> Repository;
        public RoleDBManager(IUnitOfWork<CMSContext> unitOfWork, ILogger<Log> logger, IConfiguration configuration)
        {
            Repository = new Lazy<Repository<Role>>(() => new Repository<Role>(unitOfWork, logger, configuration));
        }
        public Role GetRoleByName(string rolename)
        {
            return Repository.Value.Context.Role.FirstOrDefault(item => item.Name == rolename);
        }

    }
}

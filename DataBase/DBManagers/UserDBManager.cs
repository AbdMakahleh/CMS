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
  public class UserDBManager
    {
        public Lazy<Repository<User>> Repository;
        public UserDBManager(IUnitOfWork<CMSContext> unitOfWork,ILogger<Log> logger,IConfiguration configuration)
        {
            Repository = new Lazy<Repository<User>>(() => new Repository<User>(unitOfWork, logger, configuration));
        }

        public User GetUserIncludePolicy(long userId)
        {
         return   Repository.Value.Context.User.Include(item => item.Policy).FirstOrDefault(item => item.Id == userId);
        }
        public User GetUserByEmail(string email)
        {
            return Repository.Value.Context.User.FirstOrDefault(item => item.Email == email);
        }
    
    }
}

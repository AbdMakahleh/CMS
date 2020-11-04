using DataBase.Context;
using DataBase.Models;
using DataBase.Repository;
using DataBase.StoredProcedure;
using Infrastructure.ApiResponse;
using Infrastructure.Database;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace DataBase.DBManagers
{
  public class LookupDBManager 
    {
        public Lazy<Repository<Lookup>> Repository;
        public LookupDBManager(IUnitOfWork<CMSContext> unitOfWork,ILogger<Log> logger,IConfiguration configuration)
        {
            Repository = new Lazy<Repository<Lookup>>(() => new Repository<Lookup>(unitOfWork, logger, configuration));
        }

        public IResponseResult GetLookUpsByMajorCode(string majorCode)
        {
            IStoredProcedure getLookUpByMajorCode = new GetLookUpByMajorCode(majorCode);
            return new ResponseResult<List<JObject>>
            {
                Data = QueryExecuter.Instance.ExecuteStoredProcedure(getLookUpByMajorCode),
                Code = HttpStatusCode.OK,
                Message = "Data",
                ErrorMessage = string.Empty,
                Status = true
            };
        }
    
    }
}

using DataBase.Context;
using DataBase.Interfaces;
using DataBase.Locater;
using DataBase.Models;
using DataBase.UnitOfWork;
using Infrastructure.Entity;
using Infrastructure.Interfaces;
using Infrastructure.Proxies;
using Infrastructure.UnitOfWork;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.CommandParams
{
    public class CommandParam : ICommandParam 
    {
     
        public CommandParam(IMemoryCache memoryCache, IConfiguration config, ILogger<Log> logger, IUnitOfWork<CMSContext> unitOfWork)
        {
            
            DBManger = new Lazy<IDBMangerLocator>(() => new DBMangerLocator(unitOfWork, logger, config));
            Proxy = new Lazy<IProxyLocater>(() => new ProxyLocater(memoryCache));
            Config = config;
            MemoryCache = memoryCache;
        }

        public Lazy<IDBMangerLocator> DBManger { get; set; }
        public Lazy<IProxyLocater> Proxy { get; set; }
        public IConfiguration Config { get; set; }
        public IMemoryCache MemoryCache { get; set; }

    }
}

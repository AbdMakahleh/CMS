using DataBase.Context;
using DataBase.Interfaces;
using DataBase.Locater;
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

namespace Business.CommandParam
{
    public class CommandParam<T> : ICommandParam<T> where T : class, IEntity, new()
    {
     
        public CommandParam(IMemoryCache memoryCache, IConfiguration config, ILogger<T> logger, IUnitOfWork<CMSContext> unitOfWork)
        {
            
            DBManger = new Lazy<IDBMangerLocator<T>>(() => new DBMangerLocator<T>(unitOfWork, logger, config));
            Proxy = new Lazy<IProxyLocater>(() => new ProxyLocater(memoryCache));
            Config = config;
            MemoryCache = memoryCache;
        }

        public Lazy<IDBMangerLocator<T>> DBManger { get; set; }
        public Lazy<IProxyLocater> Proxy { get; set; }
        public IConfiguration Config { get; set; }
        public IMemoryCache MemoryCache { get; set; }

    }
}

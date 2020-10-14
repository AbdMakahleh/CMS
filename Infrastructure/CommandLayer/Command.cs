using Infrastructure.ApiResponse;
using Infrastructure.Entity;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CommandLayer
{
    public abstract class Command<Entity> : ICommand<IResponseResult, Entity> where Entity : class, IEntity, new()
    {
        public abstract IResponseResult Execute(ICommandParam<Entity> param);
        public IResponseResult BadRequset(string message, string errorCode)
        {
            throw new BadRequestException(message, errorCode);
        }
    }

    public abstract class CommandExt<Entity> : ICommandExt<IResponseResult, Entity> where Entity : class, IEntity, new()
    {
        public abstract IResponseResult Execute(ICommandParam<Entity> param, dynamic paramExt);
        public IResponseResult BadRequset(string message, string errorCode)
        {
            throw new BadRequestException(message, errorCode);
        }
    }
}

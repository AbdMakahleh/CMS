using Infrastructure.ApiResponse;
using Infrastructure.Entity;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.CommandLayer
{
    public abstract class Command : ICommand<IResponseResult> 
    {
        public abstract IResponseResult Execute(ICommandParam param);
        public IResponseResult BadRequset(string message, string errorCode)
        {
            throw new BadRequestException(message, errorCode);
        }
    }

    public abstract class CommandExt<Entity> : ICommandExt<IResponseResult>where Entity : class, IEntity, new()
    {
        public abstract IResponseResult Execute(ICommandParam param, dynamic paramExt);
        public IResponseResult BadRequset(string message, string errorCode)
        {
            throw new BadRequestException(message, errorCode);
        }
    }
}

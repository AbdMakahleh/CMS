
using Business.CommandParams;
using DataBase.Locater;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Commands.RoleCommands
{
    public class GetRoleByIDCommand : Command
    {
        public long ID { get; set; }
        public override IResponseResult Execute(ICommandParam param)
        {
            var data = (CommandParam)param;
            var result = (ResponseResult<Role>)((DBMangerLocator)data.DBManger.Value).Role.Value.Repository.Value.GetById(ID);
            if (result.Data!=null)
            {
                return new ResponseResult<object>
                {
                    Status = result.Status,
                    Code = result.Code,
                    ErrorMessage = result.ErrorMessage,
                    Message = result.Message,
                    Data = new
                    {
                        Name = result.Data.Name
                    }
                };
            }
            return result;
        }
    }
}

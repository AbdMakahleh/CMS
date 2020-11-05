using Business.CommandParam;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Commands.RoleCommands
{
    public class AddRoleCommand : Command<Role>
    {
        public string Name { get; set; }

        public override IResponseResult Execute(ICommandParam<Role> param)
        {
            var data = (CommandParam<Role>)param;
            var result = (ResponseResult<Role>)data.DBManger.Value.RespositoryUnitOfWork.Value._repo.Value.Insert(new Role
            {
                Name = Name,
            });
            if (result.Status)
            {
                return new ResponseResult<object>
                {
                    Data = new {result.Data.Name
                    },
                    Status = result.Status,
                    Message = result.Message
                    //Code = result.Code,
                    //ErrorMessage = string.Empty
                };
            }
            return result;
        }
    }
}

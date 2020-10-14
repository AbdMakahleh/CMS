using Business.CommandParam;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Commands.UserCommands
{
    public class GetUserByIDCommand : Command<User>
    {
        public long ID { get; set; }
        public override IResponseResult Execute(ICommandParam<User> param)
        {
            var result=(ResponseResult<User>) ((CommandParam<User>)param).DBManger.Value.RespositoryUnitOfWork.Value._repo.Value.GetById(ID);
            if (result.Data != null)
                return new ResponseResult<object>
                {
                    Status = result.Status,
                    Code = result.Code,
                    ErrorMessage = result.ErrorMessage,
                    Message = result.Message,
                    Data = new
                {
                       Name =result.Data.Name,
                       Id = result.Data.Id,
                       UserName = result.Data.UserName,
                       Email =result.Data.Email
                }
                };
            return result;
           
        
                
        }
    }
}

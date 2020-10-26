using Business.CommandParams;
using DataBase.Locater;
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
    public class GetUserByIDCommand : Command
    {
        public long ID { get; set; }
        public override IResponseResult Execute(ICommandParam param)
        {
            var result=(ResponseResult<User>) ((DBMangerLocator)((CommandParam)param).DBManger.Value).User.Value.Repository.Value.GetById(ID);
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

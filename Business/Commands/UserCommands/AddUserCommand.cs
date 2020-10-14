using Business.CommandParam;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Classes;
using Infrastructure.CommandLayer;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Business.Commands.UserCommands
{

    public class AddUserCommand : Command<User>
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }

        public override IResponseResult Execute(ICommandParam<User> param)
        {
            var data = (CommandParam<User>)param;
            var result =(ResponseResult<User>)data.DBManger.Value.RespositoryUnitOfWork.Value._repo.Value.Insert(new User
            {
                Name = Name,
                Password = Password,
                UserName = UserName,
                Email = Email,
                PhoneNumber = PhoneNumber,
                ProfilePicture = ProfilePicture
            });
            if(result.Status)
            return new ResponseResult<object>
            {
                Data = result.Data.MapAsJson(new List<MapSetting>()
                {
                    new MapSetting("Id",true,false,"Id"),
                    new MapSetting("Name",true,false,"Name"),
                    new MapSetting("UserName",true,false,"UserName"),
                    new MapSetting("Email",true,false,"Email")
            }, false),
                Status = result.Status,
                Message = result.Message
            };
            return result;
        }
    }
}

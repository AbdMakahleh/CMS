using Business.AuthUserCommand;
using Business.CommandParams;
using DataBase.Locater;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Classes;
using Infrastructure.CommandLayer;
using Infrastructure.Extensions;
using Infrastructure.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;

namespace Business.Commands.UserCommands
{

    public class AddUserCommand : AuthCommand
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePicture { get; set; }

        public override IResponseResult Execute(ICommandParam param)
        {
            var currentUser = (ResponseResult<User>)base.GetCurrentUser(param);
            var data = (CommandParam)param;
            var result =(ResponseResult<User>)((DBMangerLocator)data.DBManger.Value).User.Value.Repository.Value.Insert(new User
            {
                Name = Name,
                UserName = UserName,
                Password = BCrypt.Net.BCrypt.HashPassword(Password, 10),
                Email = Email,
                PhoneNumber =PhoneNumber,
                ProfilePicture = ProfilePicture,
                CreatedBy = currentUser.Data.Name,
                CreatedAt = DateTime.Now
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
                Message = result.Message,
                Code =HttpStatusCode.OK
            };
            return result;
        }
    }
}

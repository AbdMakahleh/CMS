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
using System.Linq;
using System.Text;

namespace Business.Commands.UserCommands
{
    public class GetAllUsersCommand : Command
    {
        public override IResponseResult Execute(ICommandParam param)
        {
            var data = (CommandParam)param;
            var result =(ResponseResult<IEnumerable<User>>) ((DBMangerLocator)data.DBManger.Value).User.Value.Repository.Value.GetAll();
            if (result.Data.ToList().Count > 0)
                return new ResponseResult<List<JObject>>
                {
                    Code = result.Code,
                    ErrorMessage = result.ErrorMessage,
                    Message = result.Message,
                    Status = result.Status,
                    Data = (List<JObject>)result.Data.MapAsJson(new List<MapSetting>()
                    {
                        new MapSetting("Id", true, false, "Id"),
                        new MapSetting("Name", true, false, "Name"),
                        new MapSetting("UserName", true, false, "UserName"),
                        new MapSetting("Email", true, false, "Email")
                    }, true)

                };
            return result;
        }
    }
}

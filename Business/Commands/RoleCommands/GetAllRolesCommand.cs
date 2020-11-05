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

namespace Business.Commands.RoleCommands
{
    public class GetAllRolesCommand : Command
    {
        public override IResponseResult Execute(ICommandParam param)
        {
            var data = (CommandParam)param;
            var result = (ResponseResult<IEnumerable<Role>>)((DBMangerLocator)data.DBManger.Value).Role.Value.Repository.Value.GetAll();
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
                        new MapSetting("Name", true, false, "Name")
                    }, true)

                };
            return result;
        }
    }
}

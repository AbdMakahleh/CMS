using Business.CommandParams;
using DataBase.Locater;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Commands.LookUpCommands
{
    public class GetLookupByMajorCodeCommand : Command
    {
        public string MajorCode { get; set; }
        public override IResponseResult Execute(ICommandParam param)
        {
            var commandParam = (CommandParam)param;
            var data = (DBMangerLocator)commandParam.DBManger.Value;

            return data.Lookup.Value.GetLookUpsByMajorCode(MajorCode);
        }
    }
}

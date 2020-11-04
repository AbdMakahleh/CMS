using Business.CommandParams;
using DataBase.Locater;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;
using System;

namespace Business.Commands.CMSModuleCommands
{
    public class AddCMSModuleCommand : Command
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public override IResponseResult Execute(ICommandParam param)
        {
            var commandParam = (CommandParam)param;
            var dbManager = (DBMangerLocator)commandParam.DBManger.Value;
            return dbManager.CMSModule.Value.Repository.Value.Insert(new Cmsmodule
            {
                Name = Name,
                Description = Description
            });
        }
    }
}

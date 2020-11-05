using Business.CommandParams;
using DataBase.Locater;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;

using DataBase.Models;

namespace Business.Commands.LookUpCommands
{
    public class AddLookUpCommand : Command
    {
        public string MajorCode { get; set; }
        public string Value { get; set; }
        public long CmsmoduleId { get; set; }
        public string Description { get; set; }
        public string MinorCode { get; set; }
        public long Order { get; set; }
        public override IResponseResult Execute(ICommandParam param)
        {
            var commandParam = (CommandParam)param;
            var dbManager = (DBMangerLocator)commandParam.DBManger.Value;
            return dbManager.Lookup.Value.Repository.Value.Insert(new Lookup
            {
                Description = Description,
                Value = Value,
                CmsmoduleId = CmsmoduleId,
                MinorCode = MinorCode,
                Order = Order,
                MajorCode = MajorCode
                
            });
        }
    }
}

using Business.CommandParams;
using DataBase.Locater;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;


namespace Business.Commands.RoleCommands
{
    public class UpdateRoleCommand : Command
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public override IResponseResult Execute(ICommandParam param)
        {
            var data = (CommandParam)param;
            var result = (ResponseResult<Role>)((DBMangerLocator)data.DBManger.Value).Role.Value.Repository.Value.GetById(ID);
            

            if (result.Data != null)
            {
                Role NewRole = result.Data;
                NewRole.Name = Name;
                var updatedRes = (ResponseResult<Role>)((DBMangerLocator)data.DBManger.Value).Role.Value.Repository.Value.Update(NewRole);
                return new ResponseResult<object>
                {
                    Status = result.Status,
                    Code = result.Code,
                    ErrorMessage = result.ErrorMessage,
                    Message = result.Message,
                    Data = new
                    { updatedRes.Data.Name }
                };
            }
            return new ResponseResult<object>
            {
                Status = result.Status,
                Code = result.Code,
                ErrorMessage = result.ErrorMessage,
                Message = result.Message,
                Data = new
                { }
            };
        }
    }
}

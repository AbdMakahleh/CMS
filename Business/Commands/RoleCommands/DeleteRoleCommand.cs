using Business.CommandParam;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;


namespace Business.Commands.RoleCommands
{
    public class DeleteRoleCommand : Command<Role>
    {
        public long ID { get; set; }
        public override IResponseResult Execute(ICommandParam<Role> param)
        {
            var result = (ResponseResult<Role>)((CommandParam<Role>)param).DBManger.Value.RespositoryUnitOfWork.Value._repo.Value.GetById(ID);
            if (result.Data != null)
            {
                Role NewRole = result.Data;
                NewRole.IsDeleted = true;
                var updatedRes = (ResponseResult<Role>)((CommandParam<Role>)param).DBManger.Value.RespositoryUnitOfWork.Value._repo.Value.Update(NewRole);
                return new ResponseResult<object>
                {
                    Status = result.Status,
                    Code = result.Code,
                    ErrorMessage = result.ErrorMessage,
                    Message = result.Message,
                };
            }
            return new ResponseResult<object>
            {
                Status = result.Status,
                Code = result.Code,
                ErrorMessage = result.ErrorMessage,
                Message = result.Message,
            };
        }
    }
}

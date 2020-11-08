using Business.AuthUserCommand;

using DataBase.Models;
using Infrastructure.ApiResponse;

using Infrastructure.Interfaces;
using System.Net;

namespace Business.Commands.AuthCommands
{
    public class GetCurrentUser : AuthCommand
    {

        private long _userId;
        public override IResponseResult Execute(ICommandParam param)
        {


            return new ResponseResult<long>
            {
                Data = base.GetCurrentUserId(),
                Status = true,
                Code = HttpStatusCode.OK,
                ErrorMessage = string.Empty,
                Message = "Current User"
            };
        }
    }
}

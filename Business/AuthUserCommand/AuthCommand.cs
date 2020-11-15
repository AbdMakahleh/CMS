using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;
using Business.CommandParams;
using DataBase.Locater;
using Infrastructure.Identity;
using System.Threading;
using System.Net;

namespace Business.AuthUserCommand
{
    public abstract class AuthCommand : Command
    {
        private long? _userId;
        public abstract override IResponseResult Execute(ICommandParam param);

        public IResponseResult GetCurrentUser(ICommandParam param)
        {
            var data = (CommandParam)param;
            _userId = GetCurrentUserId();
            if (_userId.HasValue)
            {
                var dbManager = ((DBMangerLocator)data.DBManger.Value);
                return dbManager.User.Value.GetUserById(_userId.Value);
            }
            return BadRequset("UnAuthorized", "405");


        }

        public IResponseResult GetCurrentUserPolicy(ICommandParam param)
        {
            var data = (CommandParam)param;
            _userId = GetCurrentUserId();
            if (_userId.HasValue)
            {
                var dbManager = ((DBMangerLocator)data.DBManger.Value);
                return new ResponseResult<User>
                {
                    Data = dbManager.User.Value.GetUserIncludePolicy(_userId.Value),
                    Status = true,
                    Code = HttpStatusCode.OK,
                    Message = "Exist"
                };
            }
            return BadRequset("UnAuthorized", "405");
        }
        public long? GetCurrentUserId()
        {
            _userId = ((UserIdentity)Thread.CurrentPrincipal?.Identity)?.Id;
            return _userId;
        }



    }
}

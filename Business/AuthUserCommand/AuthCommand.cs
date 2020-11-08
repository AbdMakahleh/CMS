using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.CommandLayer;
using Infrastructure.Interfaces;
using Business.CommandParams;
using DataBase.Locater;
using Infrastructure.Identity;
using System.Threading;

namespace Business.AuthUserCommand
{
    public abstract class AuthCommand : Command
    {
        private long _userId;
        public abstract override IResponseResult Execute(ICommandParam param);

        public User GetCurrentUser(ICommandParam param)
        {
            var data = (CommandParam)param;
            _userId = ((UserIdentity)Thread.CurrentPrincipal?.Identity).Id;
            var dbManager = ((DBMangerLocator)data.DBManger.Value);
            return ((ResponseResult<User>)dbManager.User.Value.GetUserById(_userId)).Data;
        }
        public long GetCurrentUserId()
        {
            _userId = ((UserIdentity)Thread.CurrentPrincipal?.Identity).Id;
            return _userId;
        }



    }
}

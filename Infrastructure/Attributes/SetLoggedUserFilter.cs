using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading;

namespace Infrastructure.Attributes
{
    public class SetLoggedUserFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var claim = context.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (claim != null)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(JsonConvert.DeserializeObject<UserIdentity>(claim.Value), new string[] { });
            }
        }
    }
}

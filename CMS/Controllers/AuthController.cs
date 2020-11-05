using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands.AuthCommands;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICommandParam _commandParam;
        public AuthController(ICommandParam userparam)
        {
            _commandParam = userparam;
        }

        [HttpPost()]
        public IResponseResult Login([FromBody] LoginCommand command) => command.Execute(param: this._commandParam);
    }
}

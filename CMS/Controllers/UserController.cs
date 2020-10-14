using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands.UserCommands;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ICommandParam<User> _userparam;
        public UserController(ICommandParam<User> userparam)
        {
            _userparam = userparam;
        }

        [HttpPost()]
        public IResponseResult AddUser([FromBody] AddUserCommand command) => command.Execute(param: this._userparam);

        [HttpGet()]
        public IResponseResult GetUserByID([FromQuery] GetUserByIDCommand command) => command.Execute(param: this._userparam);

        [HttpGet("GetUsers")]
        public IResponseResult GetUsers([FromQuery] GetAllUsersCommand command) => command.Execute(param: this._userparam);

    }
}

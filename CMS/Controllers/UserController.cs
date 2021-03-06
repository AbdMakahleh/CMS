﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands.AuthCommands;
using Business.Commands.UserCommands;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Attributes;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SetLoggedUserFilter]
    public class UserController : ControllerBase
    {
        private readonly ICommandParam _userparam;
        public UserController(ICommandParam userparam)
        {
            _userparam = userparam;
        }

        [HttpPost()]
        public IResponseResult AddUser([FromBody] AddUserCommand command) => command.Execute(param: this._userparam);

        [HttpGet()]
        public IResponseResult GetUserByID([FromQuery] GetUserByIDCommand command) => command.Execute(param: this._userparam);

        [HttpGet("GetUsers")]
        public IResponseResult GetUsers([FromQuery] GetAllUsersCommand command) => command.Execute(param: this._userparam);

        [HttpGet("GetCurrentUser")]
   
        public IResponseResult GetCurrentUser([FromQuery] GetCurrentUser command) => command.Execute(param: this._userparam);

    }
}

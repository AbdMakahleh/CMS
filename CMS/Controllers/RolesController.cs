using Business.Commands.RoleCommands;
using DataBase.Models;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly ICommandParam<Role> _roleparam;
        public RolesController(ICommandParam<Role> roleparam)
        {
            _roleparam = roleparam;
        }
        [HttpPost("AddRole")]
        public IResponseResult AddRole([FromBody] AddRoleCommand command) => command.Execute(param: this._roleparam);

        [HttpGet("frombody")]
        public IResponseResult GetRoleByID([FromBody] GetRoleByIDCommand command) => command.Execute(param: this._roleparam);
        [HttpGet()]
        public IResponseResult GetRoleByHeaderID([FromQuery] GetRoleByIDCommand command) => command.Execute(param: this._roleparam);

        [HttpGet("GetRoles")]
        public IResponseResult GetRoles([FromQuery] GetAllRolesCommand command) => command.Execute(param: this._roleparam);

        [HttpPut()]
        public IResponseResult UpdateRoles([FromBody]UpdateRoleCommand command) => command.Execute(param: this._roleparam);

        [HttpDelete()]
        public IResponseResult DeleteRole([FromBody] DeleteRoleCommand command) => command.Execute(param: this._roleparam);

    }
}

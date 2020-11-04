using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands.LookUpCommands;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ICommandParam _commandParam;
        public LookupController(ICommandParam userparam)
        {
            _commandParam = userparam;
        }

        [HttpPost()]
        public IResponseResult AddLookUp([FromBody] AddLookUpCommand command) => command.Execute(param: this._commandParam);

        [HttpGet()]
        public IResponseResult GetLookByMajorCode([FromQuery] GetLookupByMajorCodeCommand command) => command.Execute(param: this._commandParam);
    }
}

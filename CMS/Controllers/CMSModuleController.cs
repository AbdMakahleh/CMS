using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Business.Commands.CMSModuleCommands;
using Infrastructure.ApiResponse;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CMSModuleController : ControllerBase
    {
        private readonly ICommandParam _commandParam;
        public CMSModuleController(ICommandParam userparam)
        {
            _commandParam = userparam;
        }

        [HttpPost()]
        public IResponseResult AddCmsModule([FromBody] AddCMSModuleCommand command) => command.Execute(param: this._commandParam);

    }
}

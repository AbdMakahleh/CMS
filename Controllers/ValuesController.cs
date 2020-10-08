using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public object x = 10;


        [HttpGet] 
        public int GetX()
        {
            int y = (int)x;

            return Convert.ToInt32(y);
        }
    }
}

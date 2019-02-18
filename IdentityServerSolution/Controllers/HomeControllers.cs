using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServerSolution
{
    [Route("identity")]
    [Authorize]
    [ApiController]
    public class HomeControllers:ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from x in User.Claims select new { x.Type,x.Value});
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace BigBeer.Framework.Mvc.Authentication.Jwt.Sample.Controllers
{
    [Authorize]
    public class TestController:Controller
    {
        public JsonResult Index()
        {
            return Json("233232");
        }
    }
}

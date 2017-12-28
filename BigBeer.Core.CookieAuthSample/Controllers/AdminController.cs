using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.CookieAuthSample.Controllers
{
    public class AdminController:Controller
    {
        [Authorize]//验证过滤
        public IActionResult Index()
        {
            return View();
        }
    }
}

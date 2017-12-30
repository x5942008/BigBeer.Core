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

        public JsonResult Linq()
        {
            var a = new[] { "1", "2", "3" };
            var b = new[] { "1","2","32", "4", "5" };
            var result = a.Select(t => t).Except(b.Select(t => t));
            var result2 = b.Select(t => t).Except(a.Select(t => t));
            return Json(new { result ,result2});
        }
    }
}

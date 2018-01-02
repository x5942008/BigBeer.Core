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

        [Authorize]
        public JsonResult Te()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            foreach (var item in User.Claims)
            {
                dic.Add(item.Type,item.Value);
            }
            var result = new
            {
                a = dic,
                b = User.Identity.Name
            };
            return Json(result);
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

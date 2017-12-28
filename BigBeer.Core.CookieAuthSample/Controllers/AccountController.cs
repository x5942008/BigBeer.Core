using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace BigBeer.Core.CookieAuthSample.Controllers
{
    public class AccountController:Controller
    {
        public IActionResult AutoLogin()
        {
            var user = Request.Query["user"];
            var pwd = Request.Query["pwd"];
            if (user!="123"||pwd!="123")
            {
                return BadRequest("账号密码错误！！！");
            }
            var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name,user+pwd),
                new Claim(ClaimTypes.Role,"admin")
            };
            var claimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdentity));
            return Ok("自动登陆成功");
        }


        public IActionResult Login()
        {
            return Ok("暂时当作登陆页面");
        }

        /// <summary>
        /// 测试学习用 忽略
        /// </summary>
        /// <returns></returns>
        public JsonResult Data()
        {
            return Json(User.Identity.Name);
        }

        public IActionResult LoginOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok("成功退出登陆");
        }
    }
}

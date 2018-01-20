using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BigBeer.Core.DependencyInjection.Models;
using Microsoft.Extensions.Configuration;

namespace BigBeer.Core.DependencyInjection.Controllers
{
    public class HomeController : Controller
    {
        //服务注入
        private readonly IService _service;
        //json文件使用注入
        private readonly IConfiguration _configuration;
        /// <summary>
        /// 构造函数
        /// </summary>
       public HomeController(GetDictionaryService service,IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = _service.GetData(_configuration["test"]) ;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

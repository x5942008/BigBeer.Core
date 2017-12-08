using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Service.Core2_0.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using BigBeerServiceSample;

namespace Service.Core2_0.Controllers
{
    public class HomeController : Controller
    {
        IBigBeerService Service;
        IConfiguration Config;
        public HomeController(BigbeerFunctionOption service, IConfiguration config)
        {
            Service = service;
            Config = config;
        }

        public JsonResult X()
        {
            var KEY = "";
            var values = "";
            //HttpContext.Session.SetString(KEY, values);
            var value = Config["Data"];
            var data = Service.Do(value);
            var temp = Class.Data(value);
            var tempt = Class.Data1(value, 666);
            return Json(temp + "," + tempt + data.Redata);

        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

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

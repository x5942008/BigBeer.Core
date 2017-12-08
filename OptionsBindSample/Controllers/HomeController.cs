using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace OptionsBindSample.Controllers
{
    public class HomeController:Controller
    {
        private readonly Class _myclass;

        public HomeController(IOptions<Class> myclass)
        {
            _myclass = myclass.Value;
        }

        public IActionResult Index()
        {
            return View(_myclass);
        }
    }
}

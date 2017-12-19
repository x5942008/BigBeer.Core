using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BigBeer.Core.PayNotifys.Controllers
{
    public class AlipayController:BaseController
    {
        public JsonResult Get()
        {
            return Json(Statikey.LogPath);
        }
    }
}

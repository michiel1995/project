using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.webservice.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("home")]
        [Route("home/index")]
        public ActionResult Index()
        {
            return View();
        }
    }
}
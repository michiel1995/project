using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.webservice.Controllers
{
    public class InstellingenController : Controller
    {
        [Route("instellingen")]
        [Route("instellingen/index")]
        // GET: Instellingen
        public ActionResult Index()
        {
            return View();
        }
        [Route("instellingen/succes")]
        public ActionResult succes()
        {
            return View();
        }
    }
}
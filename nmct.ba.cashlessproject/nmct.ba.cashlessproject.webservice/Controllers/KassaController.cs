using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webservice.Models;
using nmct.ba.cashlessproject.webservice.presentationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.webservice.Controllers
{
    public class KassaController : Controller
    {
        static List<PMKassa> lijst;
        
        [Route("Kassa/{id}")]
        [Route("Kassa/index")]
        [Route("Kassa")]
        // GET: Kassa
        public ActionResult Index(int? id)
        {
            //maak api controller die organistaies tonen id en naam haal die op met jquery en maakt daar een sorteer syteem van
            lijst = KassaDA.getKassas();
            if (id.HasValue)
            {
                if(id.Value ==-1)
                    return View(lijst);
                if (id.Value == -2)
                    return View(lijst.Where(k => k.IdOrganisation == 0));
                else
                    return View(lijst.Where(k => k.IdOrganisation == id.Value));
                
            }
            else
            {
                return View(lijst);
            }
            
        }
        [Route("Kassa/nieuw")]
        [HttpGet]
        public ActionResult Nieuw()
        {
            PMKassa kassa = new PMKassa();
            kassa.Kassa = new ItRegister();
            return View(kassa);
        }
        [Route("Kassa/nieuw")]
        [HttpPost]
        public ActionResult Nieuw(PMKassa kassa)
        { 
            if(!ModelState.IsValid)
            {
                return View("Nieuw", kassa);
            }
            return View("Index", lijst);
        }

    }
}
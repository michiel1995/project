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
            //kassa.Kassa = new ItRegister();
            return View(kassa);
        }
        [Route("Kassa/nieuw")]
        [HttpPost]
        public ActionResult Nieuw(PMKassa pm)
        { 
            if(!ModelState.IsValid)
            {
                return View("Nieuw", pm);
            }
            int i = KassaDA.CreateKassa(pm.Kassa);
            if(pm.IdOrganisation != -1)
            {
                if (pm.UntilDate != -10 || pm.Fromdate != -10)
                {
                    KassaDA.KoppelKassaAanOrganisatie(pm, i);
                    //kassa aan klant database toevoegen
                }
            }
            return RedirectToAction("index");
        }

        private static PMKassa geselecteerde;
        [Route("kassa/aanpassen/{id}")]
        [HttpGet]
        public ActionResult Aanpassen(int? id)
        {
            PMKassa kassa;
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
                kassa = lijst.Find(o => o.Kassa.Id == id.Value) as PMKassa;
                geselecteerde = kassa;
            return View("Nieuw", kassa);
        }

        [Route("kassa/aanpassen/{id}")]
        [HttpPost]
        public ActionResult Aanpassen(PMKassa pm)
        {
            pm.Kassa.Id = geselecteerde.Kassa.Id;
            if (!ModelState.IsValid)
            {
                return View("Aanpassen", pm);
            }
            int i = KassaDA.ChangeKassa(pm.Kassa);
            if (pm.IdOrganisation != -1)
            {
                if (pm.UntilDate != -10 || pm.Fromdate != -10)
                {
                    int nmr = KassaDA.DeleteKoppelAanOrg(pm.Kassa.Id);
                    KassaDA.KoppelKassaAanOrganisatie(pm,pm.Kassa.Id);
                    //kassa aan klant database toevoegen
                }
            }
            else
            {
                int nmr = KassaDA.DeleteKoppelAanOrg(pm.Kassa.Id);
            }
            return RedirectToAction("index"); ;
        }


        [Route("kassa/delete/{id}")]
        [HttpPost]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
           
            PMKassa kassa= lijst.Find(o => o.Kassa.Id == id.Value ) as PMKassa;
            if (kassa != null)
            {
                KassaDA.DeleteKoppelAanOrg(id.Value);
                KassaDA.DeleteKassa(id.Value);             
            }

            return RedirectToAction("index");
        }
    }
}
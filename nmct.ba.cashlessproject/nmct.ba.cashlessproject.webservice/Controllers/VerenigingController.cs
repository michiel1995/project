using nmct.ba.cashlessproject.helper;
using nmct.ba.cashlessproject.models;
using nmct.ba.cashlessproject.webservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.webservice.Controllers
{
    public class VerenigingController : Controller
    {
        static List<ItOrganisation> organisatie;
        
        [Route("Vereniging")]
        [Route("logboek/index")]
        // GET: Verenigingen
        public ActionResult Index()
        {
            organisatie = VerenigingDA.getVerenigingen();
            return View(organisatie);
        }


        [Route("Vereniging/nieuw")]
        [HttpGet]
        public ActionResult Nieuw()
        {
            ItOrganisation org = new ItOrganisation();
            return View(org);
        }



        [Route("Vereniging/nieuw")]
        [HttpPost]
        public ActionResult Nieuw(ItOrganisation org)
        {
            if (!ModelState.IsValid)
            {
                return View ("Nieuw", org);
            }
            org.DbLogin = Cryptography.Encrypt(org.DbLogin);
            org.DbName = Cryptography.Encrypt(org.DbName);
            org.DBpass = Cryptography.Encrypt(org.DBpass);
            org.Login = Cryptography.Encrypt(org.Login);
            org.Pass = Cryptography.Encrypt(org.Pass);
            int id = VerenigingDA.NieuweVerenigingToevoegen(org);
            ItOrganisation organisat = VerenigingDA.getVereniging(id);
            OrganisatieCreateDatabase.CreateDatabase(organisat);
            organisatie = VerenigingDA.getVerenigingen();
            return View("Index",organisatie);
        }



        [Route("Vereniging/aanpassen/{id}")]
         [HttpGet]
        public ActionResult Aanpassen (int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            ItOrganisation org = organisatie.Find(o=> o.Id == id.Value) as ItOrganisation;
            return View("Nieuw",org);
        }

        [Route("Vereniging/aanpassen/{id}")]
         [HttpPost]
         public ActionResult Aanpassen(ItOrganisation org)
         {
            if(!ModelState.IsValid)
            {
                return View("Aanpassen", org);
            }
            int getal = VerenigingDA.UpdateVereniging(org);
            organisatie = VerenigingDA.getVerenigingen();
            return View("index", organisatie);
         }


        [Route("Vereniging/delete/{id}")]
        public ActionResult Delete(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction("Index");
            }
            ItOrganisation org = organisatie.Find(o => o.Id == id.Value) as ItOrganisation;
            VerenigingDA.DeleteOrganisation(org.Id);
            organisatie = VerenigingDA.getVerenigingen();
            return View("index", organisatie);
        }
    }
}
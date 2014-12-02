﻿using nmct.ba.cashlessproject.webservice.Models;
using nmct.ba.cashlessproject.webservice.presentationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.cashlessproject.webservice.Controllers
{
   
    public class LogboekController : Controller
    {
        static List<PMLogboek> lijst;

        [Route("logboek")]
        [Route("logboek/index")]
        // GET: Logboek
        public ActionResult Index()
        {
            lijst = LogboekDA.getLogs();       
            return View(lijst);
        }

         [Route("logboek/Delete/{registerid}/{timestamp}")]
        public ActionResult Delete(int? registerid, int? timestamp)
        {
             if(!timestamp.HasValue && ! registerid.HasValue)
             {
                 return View("index",lijst);
             }
            int aantal = LogboekDA.DeleteLog(registerid.Value, timestamp.Value);
            //lijst.RemoveAll(m => m.Time == tekst && m.RegisterId == hallo);
            return RedirectToAction("index");
        }

        [Route("logboek/sorteer/{waarde}")]
        public ActionResult Sorteer(string waarde)
        {
            return View("index", lijst);
        }
    }
}
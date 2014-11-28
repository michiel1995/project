using nmct.ba.cashlessproject.webservice.Models;
using nmct.ba.cashlessproject.webservice.presentationModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace nmct.ba.cashlessproject.webservice.Controllers
{
    public class OrganisatieController : ApiController
    {

        public List<PMOrganisatie> Get()
        {
            return OrganisatieDA.getOrganisaties();
        }

    }
}

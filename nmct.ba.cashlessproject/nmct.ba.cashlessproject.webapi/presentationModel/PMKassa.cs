using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.webapi.presentationModel
{
    public class PMKassa
    {
        public List<PMOrganisatie> Organisaties { get; set; }
        public List<PMRegister> Kassas { get; set; }

    }
}
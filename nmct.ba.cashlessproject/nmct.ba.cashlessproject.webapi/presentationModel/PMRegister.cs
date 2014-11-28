using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.webapi.presentationModel
{
    public class PMRegister
    {
        public ItRegister Kassa { get; set; }
        public string naamOrganisatie { get; set; }
        public string FromDate { get; set; }
        public string UntilDate { get; set; }
    }
}
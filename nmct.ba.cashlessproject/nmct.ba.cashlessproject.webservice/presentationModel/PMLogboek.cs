using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.webservice.presentationModel
{
    public class PMLogboek
    {
        public string OrganisationName { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string DatabaseName { get; set; }
        public string RegisterName { get; set; }
        public string Device { get; set; }
        public string ExpiresDate { get; set; }
        public ItErrorlog Error { get; set; }

    }
}
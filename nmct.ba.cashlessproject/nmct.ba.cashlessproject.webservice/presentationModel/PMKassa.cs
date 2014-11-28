using nmct.ba.cashlessproject.models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.webservice.presentationModel
{
    public class PMKassa
    {
        public ItRegister Kassa { get; set; }

        [DisplayName("Kies vereniging")]
        public int IdOrganisation { get; set; }
        public string OrganisationName { get; set; }
        public int Fromdate { get; set; }
        public int UntilDate { get; set; }


    }
}
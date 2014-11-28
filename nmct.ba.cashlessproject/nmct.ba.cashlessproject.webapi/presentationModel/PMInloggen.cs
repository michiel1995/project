using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nmct.ba.cashlessproject.webapi.presentationModel
{
    public class PMInloggen
    {
        [Required(ErrorMessage = "Geef gebruikersnaam in")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Geef wachtwoord in")]
        public string Password { get; set; }
    }
}
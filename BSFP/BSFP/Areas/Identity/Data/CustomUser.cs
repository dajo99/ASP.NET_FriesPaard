using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Areas.Identity.Data
{
    public class CustomUser: IdentityUser
    {
        [PersonalData]
        public string Voornaam { get; set; }
        [PersonalData]
        public string Achternaam { get; set; }
        [NotMapped]
        public string VolledigeNaam
        {
            get { return this.Voornaam + " " + this.Achternaam; }
        }
    }
}

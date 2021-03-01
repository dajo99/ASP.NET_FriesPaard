using BSFP.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Areas.Identity.Data
{
    public class CustomUser: IdentityUser
    {
        [Required]
        [PersonalData]
        public string Voornaam { get; set; }

        [Required]
        [PersonalData]
        public string Achternaam { get; set; }

        [NotMapped]
        public string VolledigeNaam
        {
            get { return this.Voornaam + " " + this.Achternaam; }
        }

        [Required]
        [PersonalData]
        public string Lidnummer { get; set; }

        //navigatieproperties
        public ICollection<Paard> Paarden { get; set; }
    }
}

using BSFP.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Paard
    {
        public int PaardID { get; set; }
        public string LocatiePaard { get; set; }
        public string Paardnaam { get; set; }
        public string Geslacht { get; set; }
        public int Leeftijd { get; set; }
        public string Levensnummer { get; set; }
        [DataType(DataType.Date)]
        public DateTime Geboortedatum { get; set; }
        public string Gebruiksdiscipline { get; set; }
        public string Niveau { get; set; }
        public int Stokmaat { get; set; }
        public int Prijs { get; set; }
        public string Informatie { get; set; }
        public DateTime Aanmaakdatum { get; set; }

        //navigatieproperties
        public string CustomUserID { get; set; }
        [ForeignKey("CustomUserID")]
        public virtual CustomUser CustomUser { get; set; }
    }
}

using BSFP.Areas.Identity.Data;
using Microsoft.AspNetCore.Http;
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
        public string Gebruiksdiscipline { get; set; }
        public string Niveau { get; set; }
        public int Stokmaat { get; set; }
        public int Prijs { get; set; }
        public string Informatie { get; set; }
        public DateTime Aanmaakdatum { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ImageName1 { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ImagePath1 { get; set; }


        [Column(TypeName = "varchar(50)")]
        public string ImageName2 { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ImagePath2 { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ImageName3 { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ImagePath3 { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ImageName4 { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ImagePath4 { get; set; }

        [NotMapped]
        public IFormFile Image1 { get; set; }
        [NotMapped]
        public IFormFile Image2 { get; set; }
        [NotMapped]
        public IFormFile Image3 { get; set; }
        [NotMapped]
        public IFormFile Image4 { get; set; }

        //navigatieproperties
        public string CustomUserID { get; set; }
        [ForeignKey("CustomUserID")]
        public virtual CustomUser CustomUser { get; set; }
    }
}

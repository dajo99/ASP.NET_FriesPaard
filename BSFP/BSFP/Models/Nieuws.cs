using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Nieuws
    {
        public int NieuwsID { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ImageName { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ImagePath { get; set; }
        [Required]
        public string Vereneging { get; set; }

        [Required]
        public string Titel_nl { get; set; }
        [Required]
        public string Titel_fr { get; set; }

        [Required]
        public string Intro_nl { get; set; }
        [Required]
        public string Intro_fr { get; set; }

        [Required]
        public string Omschrijving_nl { get; set; }
        [Required]
        public string Omschrijving_fr { get; set; }

        public DateTime Datum { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}

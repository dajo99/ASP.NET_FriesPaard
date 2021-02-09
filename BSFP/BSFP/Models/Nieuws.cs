using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Nieuws
    {
        public int NieuwsID { get; set; }

        public string Image { get; set; }

        [Required]
        public string Titel { get; set; }

        [Required]
        public string Intro { get; set; }

        [Required]
        public string Omschrijving { get; set; }

        public DateTime Datum { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Tarief
    {
        public int TariefID { get; set; }
        [Required]
        public string Omschrijving_nl { get; set; }
        [Required]
        public string Omschrijving_fr { get; set; }
        [Required]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Prijs { get; set; }

        [Required]
        [DefaultValue(true)]
        public bool IsTeruggave { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Nieuws
    {
        public int NieuwsID { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
    }
}

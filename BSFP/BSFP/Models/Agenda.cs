using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Agenda
    {
        public int AgendaID { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Datum { get; set; }
        public DateTime Starttijd { get; set; }
        public DateTime Eindtijd { get; set; }
        public string Locatie { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Agenda
    {
        public int AgendaID { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }

        [DataType(DataType.Date)]
        public DateTime Datum { get; set; }

        [DataType(DataType.Time)]
        public DateTime Starttijd { get; set; }
        
        [DataType(DataType.Time)]
        public DateTime Eindtijd { get; set; }
        public string Locatie { get; set; }
    }
}

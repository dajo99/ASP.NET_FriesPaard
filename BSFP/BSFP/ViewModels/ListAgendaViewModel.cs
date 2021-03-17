using BSFP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.ViewModels
{
    public class ListAgendaViewModel
    {
        public string SearchName { get; set; }
        public string SearchPlace { get; set; }
        public DateTime? SearchDate { get; set; }
        public List<Agenda> AgendaLijst { get; set; }
    }
}

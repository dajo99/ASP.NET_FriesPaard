using BSFP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.ViewModels
{
    public class ListNieuwsViewModel
    {
        public string Search { get; set; }
        public List<Nieuws> NieuwsLijst { get; set; }
    }
}

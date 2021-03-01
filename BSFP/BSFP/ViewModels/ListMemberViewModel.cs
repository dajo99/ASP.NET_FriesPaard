using BSFP.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.ViewModels
{
    public class ListMemberViewModel
    {
        public string AchternaamSearch { get; set; }
        public string VoornaamSearch { get; set; }
        public List<CustomUser> Members { get; set; }
    }
}

using BSFP.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.ViewModels
{
    public class EditMemberViewModel
    {
        public EditMemberViewModel()
        {
            Claims = new List<string>(); Roles = new List<string>();
        }
        public CustomUser Member { get; set; }

        public IList<string> Roles { get; set; }

        public List<string> Claims { get; set; }
    }
}

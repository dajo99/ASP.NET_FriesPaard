using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BSFP.Models
{
    public class Sponsor
    {
        public int SponsorID { get; set; }
        public string Titel { get; set; }
        public string Omschrijving { get; set; }
        public string WebsiteLink { get; set; }

        [Column(TypeName = "varchar(50)")]
        public string ImageName { get; set; }

        [Column(TypeName = "varchar(250)")]
        public string ImagePath { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}

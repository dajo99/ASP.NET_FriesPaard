using BSFP.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSFP.Models;

namespace BSFP.Data
{
    public class BSFPContext: IdentityDbContext<CustomUser>
    {
        public BSFPContext(DbContextOptions<BSFPContext> options) : base(options)
        {
        }

       


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
          
        }

       


        public DbSet<BSFP.Models.Agenda> Agenda { get; set; }
    }
}

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

        public DbSet<Agenda> Agenda { get; set; }
        public DbSet<Nieuws> Nieuws { get; set; }
        public DbSet<Sponsor> Sponsors { get; set; }
        public DbSet<Paard> Paarden { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Agenda>().ToTable("Agenda");
            modelBuilder.Entity<Nieuws>().ToTable("Nieuws");
            modelBuilder.Entity<Sponsor>().ToTable("Sponsors");
            modelBuilder.Entity<Nieuws>().ToTable("Nieuws");
            modelBuilder.Entity<Paard>().ToTable("Paarden");

        }


    }
}

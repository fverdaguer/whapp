using ManyToMany.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace ManyToMany.DAL
{
    public class MtoMEntities : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<Bat> Bats { get; set; }
        public DbSet<Team> Teams { get; set; }

        public DbSet<Project> Projects { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Instrument> Instruments { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //This option keeps table names in singular form, my personal preference.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
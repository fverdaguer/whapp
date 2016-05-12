namespace Whapp.DAL
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    using Whapp.Models;

    public class MtoMEntities : DbContext
    {
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
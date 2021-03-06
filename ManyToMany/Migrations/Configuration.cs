namespace ManyToMany.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Whapp.DAL;
    using Whapp.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<MtoMEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MtoMEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Genres.AddOrUpdate(g => g.Name,
                new Genre { Name = "Rock" },
                new Genre { Name = "Punk" },
                new Genre { Name = "Jazz" },
                new Genre { Name = "Cumbia" }
                );

            context.Instruments.AddOrUpdate(i => i.Name,
                new Instrument { Name = "Lead Guitar" },
                new Instrument { Name = "Rythm Guitar" },
                new Instrument { Name = "Vocals" },
                new Instrument { Name = "Bass" },
                new Instrument { Name = "Drums" }
                );
        }
    }
}

namespace ManyToMany.Migrations
{
    using ManyToMany.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ManyToMany.DAL.MtoMEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ManyToMany.DAL.MtoMEntities context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.

            context.Teams.AddOrUpdate(
              p => p.TeamName,
              new Team { TeamName="Blackhawks" },
              new Team { TeamName="Jays" }
            );
            context.SaveChanges();

            context.Players.AddOrUpdate(p=>p.PlayerLName,
                new Player { PlayerFName="Joe", PlayerLName="Blow", DOB=DateTime.Parse("1990-01-01"), TeamID=2 },
                new Player { PlayerFName="Sam", PlayerLName="Smith", DOB=DateTime.Parse("1990-01-01"), TeamID=2 },
                new Player { PlayerFName="Fred", PlayerLName="Flintstone", DOB=DateTime.Parse("1990-01-01"), TeamID=1 }
            );
            context.SaveChanges();

            context.Bats.AddOrUpdate(b => b.BatName,
                new Bat { BatName = "Wonderboy" },
                new Bat { BatName = "Hard-Hitter" },
                new Bat { BatName = "Slugger" }
                );
            context.SaveChanges();

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

namespace MaouHeroLanding.Migrations
{
    using MaouHeroLanding.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MaouHeroLanding.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "MaouHeroLanding.Models.ApplicationDbContext";
        }

        protected override void Seed(MaouHeroLanding.Models.ApplicationDbContext context)
        {
            var artigos = new List<Artigos> {
               new Artigos {ID=1, Nome=""},
               new Artigos {ID=2, Nome=""},
               new Artigos {ID=3, Nome=""}
            };
            /*artigos.ForEach(aa => context.Artigos.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();*/
        }
    }
}

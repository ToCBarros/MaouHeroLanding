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
               new Artigos {ID=1, Nome="Anime Body Pillow",Tipo="Dakimakura",Preco=9.99M,Data_Entrada=new DateTime(1999,6,10),imagem="AnimeBodyPillow.jpg",Descricao="almofada",Produtor="Bem fofa"},
               new Artigos {ID=2, Nome="My Hero Academia",Tipo="Mangá",Preco=20M,Data_Entrada=new DateTime(1999,6,2),imagem="MyHeroAcademia.jpg",Descricao="BD",Produtor="banda desenhada"},
               new Artigos {ID=3, Nome="Miss Kobayashis Dragon Maid",Tipo="Light Novel",Preco=5M,Data_Entrada=new DateTime(2010,9,9),imagem="MissKobayashisDragonMaid.jpg",Descricao="lewd",Produtor="Nope"}
            };
            artigos.ForEach(aa => context.Artigos.AddOrUpdate(a => a.Nome, aa));
            context.SaveChanges();
            

        
        }
    }
}

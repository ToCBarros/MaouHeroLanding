using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace MaouHeroLanding.Models
{
    public class Compras
    {
        public int id { get; set; }

        public decimal preco { get; set; }

        [ForeignKey("Encomendas")]
        public int EncomendaFK { get; set; }
        public Encomendas Encomenda { get; set; }

        [ForeignKey("Artigo")]
        public int ArtigoFK { get; set; }
        public Artigos Artigo { get; set; }
    }
}
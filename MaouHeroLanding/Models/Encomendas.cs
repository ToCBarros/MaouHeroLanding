using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

using System.ComponentModel.DataAnnotations;
using System.Web;

namespace MaouHeroLanding.Models
{
    public class Encomendas
    {
        public int ID { get; set; }

        public string Local_entrega { get; set; }

        public Decimal Preco { get; set; }

        public Boolean Estado { get; set; }

        public ICollection<Compras> ListaDasEncomendas { get; set; }

        [ForeignKey("Cliente")]
        public int ClienteFK { get; set; }
        public Clientes Cliente { get; set; }

        
    }
}
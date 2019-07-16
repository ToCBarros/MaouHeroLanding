using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;

namespace MaouHeroLanding.Models
{
    public class Artigos
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string Tipo { get; set; }

        public decimal Preco { get; set; }

        public DateTime Data_Entrada { get; set; }

        public string imagem { get; set; }

        public string Descricao { get; set; }

        public string Produtor { get; set; }

        public ICollection<Compras> ListaDasCompras { get; set; }
    }
}
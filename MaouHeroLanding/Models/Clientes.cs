using System;
using System.Collections.Generic;
using System.Web;

namespace MaouHeroLanding.Models
{
    public class Clientes
    {
        public int ID { get; set; }

        public string Nome { get; set; }

        public string NIF { get; set; }

        public DateTime Data_Nasc { get; set; }

        public string Telemovel { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Codigo_postal { get; set; }

        public ICollection<Encomendas> ListaDasEncomendas { get; set; }
    }
}
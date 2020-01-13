using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API_ConsumoServicosERP.Models
{
    public class Cliente
    {
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public Cliente(string nomePar, string telefonePar)
        {
            this.Nome = nomePar;
            this.Telefone = telefonePar;
        }
    }
}
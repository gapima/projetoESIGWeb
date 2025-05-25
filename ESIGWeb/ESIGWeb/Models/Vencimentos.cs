using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESIGWeb.Models
{
    public class Vencimentos
    {
        public int id { get; set; }
        public string Descricao { get; set; }
        public decimal Valor { get; set; }
        public string FormaIncidencia { get; set; }
        public string Tipo { get; set; }
        public List<CargoVencimento> CargoVencimento { get; set; }

    }
}
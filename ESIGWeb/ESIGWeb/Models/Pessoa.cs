using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESIGWeb.Models
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int CargoId { get; set; }
        public string CargoNome { get; set; }
    }
}
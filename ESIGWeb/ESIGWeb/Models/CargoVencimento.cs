using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ESIGWeb.Models
{
    public class CargoVencimento
    {
        public int Id { get; set; }
        public int CargoId { get; set; }
        public int VencimentoId { get; set; }
    }
}
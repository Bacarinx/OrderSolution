using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Requests
{
    public class RequestProductUpdate
    {
        public decimal? Price { get; set; }
        public string? Name { get; set; } = String.Empty;
        public bool? Active { get; set; }
    }
}
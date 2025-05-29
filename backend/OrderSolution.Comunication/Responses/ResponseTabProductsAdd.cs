using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseTabProductsAdd
    {
         public string Tab { get; set; } = String.Empty;
        public string Product { get; set; } = String.Empty;
        public DateTime Time { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Requests
{
    public class RequestTabProductsAdd
    {
        public int TabId { get; set; }
        public int ProductId { get; set; }
        public int ServiceId { get; set; }
    }
}
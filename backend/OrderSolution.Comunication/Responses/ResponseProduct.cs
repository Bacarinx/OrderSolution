using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseProduct
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public decimal Price { get; set; }
        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseProductsOnTab
    {
        public int TabProductId { get; set; }
        public string ProductName { get; set; } = String.Empty;
        public DateTime InsertionDate { get; set; }
        public decimal Value { get; set; }
        public bool IsActive { get; set; }
    }
}
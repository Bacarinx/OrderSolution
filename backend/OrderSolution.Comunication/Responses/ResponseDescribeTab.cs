using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseDescribeTab
    {
        public int TabId { get; set; }
        public string Code { get; set; } = String.Empty;
        public string? ClientName { get; set; } = String.Empty;
        public string? ClientCPF { get; set; } = String.Empty;
        public bool? IsOpen { get; set; }
        public decimal Value { get; set; }
        public List<ResponseProductsOnTab> Products { get; set; } = [];
    }
}
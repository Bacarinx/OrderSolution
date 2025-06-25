using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseTab
    {
        public string Code { get; set; } = String.Empty;
        public string? ClientName { get; set; } = String.Empty;
        public string? ClientCPF { get; set; } = String.Empty;
        public int UserId { get; set; }
        public decimal Value { get; set; }
        public bool? IsOpen { get; set; }
    }
}
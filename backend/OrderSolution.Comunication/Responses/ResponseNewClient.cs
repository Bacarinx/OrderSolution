using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseNewClient
    {
        public string Name { get; set; } = String.Empty;
        public string CPF { get; set; } = String.Empty;
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string Gender { get; set; } = String.Empty;
    }
}
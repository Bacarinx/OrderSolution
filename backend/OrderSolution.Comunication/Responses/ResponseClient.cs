using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseClient
    {
        public int ClientId { get; set; }
        public string Name { get; set; } = String.Empty;
        public string CPF { get; set; } = String.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Gender { get; set; } = String.Empty;
    }
}
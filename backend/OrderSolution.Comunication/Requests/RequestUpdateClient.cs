using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Requests
{
    public class RequestUpdateClient
    {
        public string? Name { get; set; } = String.Empty;
        public string? Email { get; set; } = String.Empty;
        public string? PhoneNumber { get; set; } = String.Empty;
        public string? Gender { get; set; } = String.Empty;
    }
}
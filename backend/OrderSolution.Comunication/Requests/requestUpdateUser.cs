using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Requests
{
    public class requestUpdateUser
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string CNPJ { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
    }
}
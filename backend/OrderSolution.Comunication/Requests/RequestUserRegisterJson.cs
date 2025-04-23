using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Requests
{
    public class RequestUserRegisterJson
    {
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public string Password { get; set; } = String.Empty;
        public string CNPJ { get; set; } = String.Empty;
        public string Address { get; set; } = String.Empty;
    }
}

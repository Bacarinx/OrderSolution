using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseUserRegisterJson
    {
        public string Name { get; set; } = String.Empty;
        public string AccessToken { get; set; } = String.Empty;
    }
}

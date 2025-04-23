using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{

    public class ExceptionUserUnathorized : OrderSolutionException
    {
        public ExceptionUserUnathorized() : base("Usuário não encontrado e/ou não autorizado!") { }
        public override List<string> GetMessage() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
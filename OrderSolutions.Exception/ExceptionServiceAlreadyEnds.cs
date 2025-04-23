using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionServiceAlreadyEnds : OrderSolutionException
    {
        public ExceptionServiceAlreadyEnds() : base("Serviço já foi finalizado anteriormente!") { }

        public override List<string> GetMessage() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
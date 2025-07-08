using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionCantEndService : OrderSolutionException
    {
        public ExceptionCantEndService() : base("Não é possível encerrar o servico. Há itens a serem pagos!") { }
        public override List<string> GetMessage() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
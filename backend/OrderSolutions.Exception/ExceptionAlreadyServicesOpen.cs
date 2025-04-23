using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception.obj
{
    public class ExceptionAlreadyServicesOpen : OrderSolutionException
    {
        public ExceptionAlreadyServicesOpen() : base("Já existe um serviço aberto! Feche-o antes de abrir outro.") { }
        public override List<string> GetMessage() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
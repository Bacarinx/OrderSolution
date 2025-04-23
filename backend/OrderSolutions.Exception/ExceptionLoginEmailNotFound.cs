using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionLoginEmailNotFound : OrderSolutionException
    {
        public ExceptionLoginEmailNotFound() : base("Email ou Senha inválidos!") { }

        public override List<string> GetMessage() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
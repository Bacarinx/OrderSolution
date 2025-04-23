using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionProductsNotFound : OrderSolutionException
    {
        public ExceptionProductsNotFound() : base("Não há produtos cadastrados!") { }

        public override List<string> GetMessage() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NoContent;
    }
}
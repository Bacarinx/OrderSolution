using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionCategory : OrderSolutionException
    {
        public ExceptionCategory() : base("Essa categoria já existe!") { }
        public override List<string> GetMessage() => [Message];
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
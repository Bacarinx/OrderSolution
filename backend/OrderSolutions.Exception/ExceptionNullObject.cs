using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionNullObject : OrderSolutionException
    {
        public ExceptionNullObject(string message) : base(message) { }
        public override List<string> GetMessage() => [Message];

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
    }
}
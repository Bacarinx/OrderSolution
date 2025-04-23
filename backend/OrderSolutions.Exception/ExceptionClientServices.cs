using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionClientServices : OrderSolutionException
    {
        private readonly List<string> _errors;
        public ExceptionClientServices(List<String> message) : base(string.Empty)
        {
            _errors = message;
        }
        public override List<string> GetMessage() => _errors;
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{

    public class ExceptionNoContentMessage : OrderSolutionException
    {
        private readonly List<String> _erros;

        public ExceptionNoContentMessage(List<String> errors) : base(String.Empty)
        {
            _erros = errors;
        }

        public override List<string> GetMessage() => _erros;
        public override HttpStatusCode GetStatusCode() => HttpStatusCode.NoContent;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public class ExceptionUserRegister : OrderSolutionException
    {
        private readonly List<String> _Erros;

        public ExceptionUserRegister(List<String> errors) : base(String.Empty)
        {
            _Erros = errors;
        }

        public override List<string> GetMessage() => _Erros;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.Unauthorized;
    }
}

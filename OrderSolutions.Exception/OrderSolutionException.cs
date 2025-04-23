using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OrderSolutions.Exception
{
    public abstract class OrderSolutionException : SystemException
    {
        protected OrderSolutionException(){}
        protected OrderSolutionException(string message) : base(message) { }
        
        public abstract List<String> GetMessage();
        public abstract HttpStatusCode GetStatusCode();
    }
}

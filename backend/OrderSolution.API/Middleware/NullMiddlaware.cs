using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolutions.Exception;

namespace OrderSolution.API.Middleware
{
    public class NullMiddlaware
    {
        public void Execute<T>(T obj, string objName)
        {
            if (obj == null)
            {
                var message = objName + " não existe";
                throw new ExceptionNullObject(message);
            }
        }
    }
}
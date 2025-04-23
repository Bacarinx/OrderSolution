using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context) {
            if(context.Exception is OrderSolutionException ex){
                context.HttpContext.Response.StatusCode = (int)ex.GetStatusCode();
                context.Result = new ObjectResult(new ExceptionRegisterUserResponse
                {
                    Errors = ex.GetMessage()
                });
            }
        }
    }
}
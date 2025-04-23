using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OrderSolution.API.Context;
using OrderSolution.API.UseCases.Login;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
#pragma warning disable IDE0290
        public LoginController(OrderSolutionDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseUserRegisterJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Login(RequestUserLoginJson request)
        {
            var useCase = new UseCaseUserLogin();
            var response = useCase.Execute(_context, request);
            return Ok(response);
        }
    }
}
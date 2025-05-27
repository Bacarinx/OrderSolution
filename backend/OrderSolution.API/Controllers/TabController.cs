using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.UseCases.Tab;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TabController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly HttpContextAccessor _httpContext;
        public TabController(OrderSolutionDbContext context, HttpContextAccessor httpcontext)
        {
            _context = context;
            _httpContext = httpcontext;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Create(string code)
        {
            var useCase = new UseCaseTab(_context, _httpContext);
            useCase.CreateTab(code);
            return Create(code);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Remove(int id)
        {
            var useCase = new UseCaseTab(_context, _httpContext);
            useCase.RemoveTab(id);
            return Ok("Comanda Removida com Sucesso!");
        }

        [HttpPatch]
        [Authorize]
        [ProducesResponseType(typeof(Client), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status400BadRequest)]
        public IActionResult Update(RequestTabChangeClient request)
        {
            var useCase = new UseCaseTab(_context, _httpContext);
            useCase.TrocarPessoaComanda(request);
            return Ok(_context.Clients.FirstOrDefault(c => c.Id == request.clientId));
        }
    }
}
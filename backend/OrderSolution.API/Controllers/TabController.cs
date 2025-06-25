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
    public class TabController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpContext;
        public TabController(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpContext = httpcontext;
        }

        [HttpPost]
        [Authorize]
        [Route("[controller]")]
        public IActionResult Create(RequestNewTab code)
        {
            var useCase = new UseCaseTab(_context, _httpContext);
            var codes = useCase.CreateTab(code);
            return Ok(codes);
        }

        [HttpDelete]
        [Authorize]
        [Route("[controller]/{id}")]
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
        [Route("[controller]/{id}")]
        public IActionResult Update(int id, RequestTabChangeClient request)
        {
            var useCase = new UseCaseTab(_context, _httpContext);
            useCase.TrocarPessoaComanda(id, request);
            return Ok(_context.Clients.FirstOrDefault(c => c.Id == request.clientId));
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ResponseGetTabs), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("[controller]")]
        public IActionResult GetTabs(int pagenumber, string? code)
        {
            var useCase = new UseCaseTab(_context, _httpContext);
            var res = useCase.GetTabs(pagenumber, code ?? String.Empty);

            if (res.Qtd > 0)
            {
                return Ok(res);
            }

            return NoContent();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(ResponseDescribeTab), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Route("[controller]/{code}")]
        public IActionResult GetTabs(string code)
        {
            var useCase = new UseCaseTab(_context, _httpContext);
            var res = useCase.GetTab(code);
            return Ok(res);
        }
    }
}
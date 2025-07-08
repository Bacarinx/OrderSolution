using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.Middleware;
using OrderSolution.API.UseCases.TabProducts;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TabProductsController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;

        public TabProductsController(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ResponseTabProductsAdd), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status400BadRequest)]
        public IActionResult AddProduct(List<RequestTabProductsAdd> requestList)
        {
            var useCase = new UseCaseTabProducts(_context, _httpcontext);
            var list = useCase.AddProductToTab(requestList);
            return Created(String.Empty, list);
        }

        [HttpPatch]
        [Authorize]
        public IActionResult CancelProduct(int idProductTab)
        {
            var useCase = new UseCaseTabProducts(_context, _httpcontext);
            useCase.CancelProduct(idProductTab);
            return Ok("Produto cancelado.");
        }

        [HttpPut]
        [Authorize]
        [Route("{tabId}")]
        public IActionResult EndTab(int tabId)
        {
            var useCase = new UseCaseTabProducts(_context, _httpcontext);
            useCase.TabPayment(tabId);
            return Ok("Comanda encerrada com sucesso!");
        }
    }
}
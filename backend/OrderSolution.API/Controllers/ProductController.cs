using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.UseCases.Product;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

#pragma warning disable IDE0290
        public ProductController(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<ResponseProduct>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public IActionResult ListProducts()
        {
            var useCase = new UseCaseProduct(_context, _httpContext);
            var produtos = useCase.ListarProdutosPorCategoria();
            return Ok(produtos);
        }

        [HttpPost]
        [ProducesResponseType(typeof(RequestNewProduct), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status400BadRequest)]
        [Authorize]
        public IActionResult CriarProduto(RequestNewProduct request)
        {
            var useCase = new UseCaseProduct(_context, _httpContext);
            useCase.CriarProduto(request);
            return Created(String.Empty, request);
        }

        [HttpDelete]
        [Authorize]
        [Route("id")]
        public IActionResult RemoverProduto(int id)
        {
            var useCase = new UseCaseProduct(_context, _httpContext);
            useCase.RemoverProduto(id);
            return Ok("Produto Removido com sucesso!");
        }
    }
}
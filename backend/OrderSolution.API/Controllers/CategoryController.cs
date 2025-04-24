using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.UseCases.Category;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

#pragma warning disable IDE0290
        public CategoryController(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseCategory), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public IActionResult Create(RequestCategory request)
        {
            var useCase = new UseCaseCategory(_context, _httpContext);
            var response = useCase.CriarCategoria(request);
            return Created(String.Empty, response);
        }

        [HttpDelete]
        [Authorize]
        public IActionResult Delete(int categoryId)
        {
            var useCase = new UseCaseCategory(_context, _httpContext);
            useCase.DeleteCategory(categoryId);
            return Ok(categoryId);
        }

        [HttpPatch]
        [Authorize]
        public IActionResult Update(RequestUpdateCategory request)
        {
            var useCase = new UseCaseCategory(_context, _httpContext);
            useCase.UpdateCategory(request);
            return Ok(request);
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCategory()
        {
            var useCase = new UseCaseCategory(_context, _httpContext);
            var response = useCase.GetCategory();
            return Ok(response);
        }
    }
}
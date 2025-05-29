using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.Middleware;
using OrderSolution.API.UseCases.TabProducts;

namespace OrderSolution.API.Controllers.EndTab
{
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;

        public PaymentController(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;
        }

        [HttpPatch]
        [Authorize]
        [Route("Tab/[controller]/{tabId}")]
        public IActionResult PayTab(int tabId)
        {
            var useCase = new UseCaseTabProducts(_context, _httpcontext);
            useCase.TabPayment(tabId);
            return Ok();
        }
    }
}
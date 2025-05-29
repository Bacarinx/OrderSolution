using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.UseCases.Services;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    [Authorize]
    public class ServiceController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;
        public ServiceController(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;
        }

        [HttpPost]
        [Route("[controller]")]
        [ProducesResponseType(typeof(ResponseService), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult CreateService()
        {
            var useCase = new UseCaseServices(_context, _httpcontext);
            var response = useCase.StartService();
            return Created(string.Empty, response);
        }

        [HttpPatch]
        [Route("[controller]/{ServiceId}")]
        [ProducesResponseType(typeof(ResponseService), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult EndService(int ServiceId)
        {
            var useCase = new UseCaseServices(_context, _httpcontext);
            var response = useCase.EndService(ServiceId);
            return Ok(response);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.UseCases.ServiceClient;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ServiceClientsController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;
        public ServiceClientsController(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpcontext = httpContext;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseClientService), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status401Unauthorized)]
        [Authorize]
        public IActionResult Create(RequestClientService request)
        {
            var useCase = new UseCaseServiceClient(_context, _httpcontext);
            useCase.AddClientInService(request.ServiceId, request.ClientId);

            return Created(String.Empty, new { request.ServiceId, request.ClientId });
        }
    }
}
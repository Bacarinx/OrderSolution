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
        [Route("[controller]")]
        public IActionResult Create(RequestClientService request)
        {
            var useCase = new UseCaseServiceClient(_context, _httpcontext);
            useCase.AddClientInService(request.ServiceId, request.ClientId);

            return Created(String.Empty, new { request.ServiceId, request.ClientId });
        }

        [HttpDelete]
        [Authorize]
        [Route("[controller]/{serviceClientId}")]
        public IActionResult Remove(int serviceClientId)
        {
            var useCase = new UseCaseServiceClient(_context, _httpcontext);
            useCase.RemoveClientFromService(serviceClientId);
            return Ok("Cliente Retirado do servico com sucesso!");
        }

        [HttpGet]
        [Authorize]
        [Route("[controller]/{serviceId}")]
        public IActionResult GetClients(int serviceId)
        {
            var useCase = new UseCaseServiceClient(_context, _httpcontext);
            var res = useCase.GetServiceClients(serviceId);
            return Ok(res);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.UseCases.Client;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;
        public ClientController(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;
        }
        [Route("[controller]")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(ResponseNewClient), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult AddClient(RequestNewClient req)
        {
            var useCase = new UseCaseClient(_context, _httpcontext);
            var res = useCase.AddClient(req);
            return Created(String.Empty, res);
        }

        [HttpDelete]
        [Authorize]
        [Route("[controller]/{ClientId}")]
        public IActionResult RemoveClient(int ClientId)
        {
            var useCase = new UseCaseClient(_context, _httpcontext);
            useCase.RemoveClient(ClientId);
            return Ok("Cliente removido com sucesso!");
        }

        [HttpPatch]
        [Authorize]
        [Route("[controller]/{ClientId}")]
        public IActionResult UpdateClient(int ClientId, RequestUpdateClient req)
        {
            var useCase = new UseCaseClient(_context, _httpcontext);
            var res = useCase.UpdateClient(ClientId, req);
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("[controller]")]
        public IActionResult GetClients()
        {
            var useCase = new UseCaseClient(_context, _httpcontext);
            var res = useCase.GetClients();
            return Ok(res);
        }

        [HttpGet]
        [Authorize]
        [Route("[controller]/{ClientId}")]
        public IActionResult GetOneClient(int ClientId)
        {
            var useCase = new UseCaseClient(_context, _httpcontext);
            var res = useCase.GetOneClient(ClientId);
            return Ok(res);
        }
    }
}
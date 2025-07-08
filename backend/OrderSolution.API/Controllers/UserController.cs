using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.UseCases.User;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;

namespace OrderSolution.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IHttpContextAccessor _httpcontext;
        private readonly OrderSolutionDbContext _context;
        public UserController(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(typeof(Entities.User), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status401Unauthorized)]
        public IActionResult GetUser()
        {
            var usecase = new UseCaseUser(_context, _httpcontext);
            var res = usecase.getUser();
            return Ok(res);
        }

        [HttpPatch]
        [Authorize]
        public IActionResult UpdateUser(requestUpdateUser req)
        {
            var usecase = new UseCaseUser(_context, _httpcontext);
            usecase.UpdateUser(req);
            return Ok("Usuário Atualizado com Sucesso!");
        }
    }
}
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderSolution.API.Context;
using OrderSolution.API.UseCases.User;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class RegisterController : ControllerBase
    {

        private readonly OrderSolutionDbContext _context;

        public RegisterController(OrderSolutionDbContext context)
        {
            _context = context;
        }

        [ProducesResponseType(typeof(ResponseUserRegisterJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ExceptionRegisterUserResponse), StatusCodes.Status401Unauthorized)]
        [HttpPost]
        public IActionResult Register(RequestUserRegisterJson request)
        {
            var useCase = new UseCaseRegister();
            var response = useCase.Execute(request, _context);
            return Created(String.Empty, response);
        }
    }
}

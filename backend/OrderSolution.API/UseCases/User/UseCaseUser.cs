using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Middleware;
using OrderSolution.API.Services.LoggedUser;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.User
{
    public class UseCaseUser
    {
        private readonly IHttpContextAccessor _httpcontext;
        private readonly OrderSolutionDbContext _context;
        private readonly Entities.User User;
        public UseCaseUser(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;

            var loggedUser = new LoggedUserService(_httpcontext);
            User = loggedUser.GetUser(_context);
        }

        public Entities.User getUser()
        {
            if (User != null)
            {
                return User;
            }
            else
            {
                throw new ExceptionUserRegister(["Usuário não logado"]);
            }

        }
    }
}
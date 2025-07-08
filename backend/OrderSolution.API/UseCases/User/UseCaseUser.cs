using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Middleware;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.Comunication.Requests;
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

        public void UpdateUser(requestUpdateUser req)
        {
            if (User == null)
                throw new ExceptionUserRegister(["Usuário não logado"]);

            if (!String.IsNullOrEmpty(req.Name)) User.Name = req.Name;
            if (!String.IsNullOrEmpty(req.Email)) User.Email = req.Email;
            if (!String.IsNullOrEmpty(req.CNPJ)) User.CNPJ = req.CNPJ;
            if (!String.IsNullOrEmpty(req.Address)) User.Address = req.Address;

            _context.SaveChanges();
        }
    }
}
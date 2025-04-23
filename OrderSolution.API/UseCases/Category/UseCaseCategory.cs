using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using OrderSolution.API.Context;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.Category
{
    public class UseCaseCategory
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

#pragma warning disable IDE0290
        public UseCaseCategory(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public string CriarCategoria(RequestCategory request)
        {
            var CategoryAlreadyExists = _context.Categories.FirstOrDefault(cat => cat.Name == request.Name);
            if (CategoryAlreadyExists != null)
            {
                throw new ExceptionCategory();
            }

            var loggedUser = new LoggedUserService(_httpContext);
            var user = loggedUser.getUser(_context);

            var response = new Entities.Category
            {
                UserId = user.Id,
                Name = request.Name
            };

            _context.Categories.Add(response);
            _context.SaveChanges();

            return response.Name;
        }
    }
}
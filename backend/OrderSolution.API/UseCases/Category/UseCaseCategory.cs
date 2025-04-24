using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Interfaces;
using OrderSolution.API.Middleware;
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

        public void DeleteCategory(int categoryId)
        {
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.getUser(_context);

            var nullMiddlaware = new NullMiddlaware();
            var userMiddlaware = new UserMiddlaware();

            var categorieToBeRemoved = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            nullMiddlaware.Execute(categorieToBeRemoved, "Produto");
            userMiddlaware.Execute(ActualLoggedUser, categorieToBeRemoved);

            _context.Categories.Remove(categorieToBeRemoved);
            _context.SaveChanges();
        }

        public void UpdateCategory(RequestUpdateCategory request)
        {
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.getUser(_context);

            var nullMiddlaware = new NullMiddlaware();
            var userMiddlaware = new UserMiddlaware();

            var categorieToBeUpdate = _context.Categories.FirstOrDefault(c => c.Id == request.CategoryId);

            nullMiddlaware.Execute(categorieToBeUpdate, "Produto");
            userMiddlaware.Execute(ActualLoggedUser, categorieToBeUpdate);

            categorieToBeUpdate.Name = request.NewName;
            _context.SaveChanges();
        }

        public ResponseGetCategories GetCategory()
        {
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.getUser(_context);

            var userMiddlaware = new UserMiddlaware();
            userMiddlaware.Execute<IOwnedUserId>(ActualLoggedUser, null);

            var query = _context.Categories.Where(c => c.UserId == ActualLoggedUser.Id);

            List<ModelCategory> categories = [];
            foreach (var q in query)
            {
                categories.Add(new ModelCategory{
                    CategoryId = q.Id,
                    CategoryName = q.Name
                });
            }

            return new ResponseGetCategories
            {
                Categories = categories
            };
        }
    }
}
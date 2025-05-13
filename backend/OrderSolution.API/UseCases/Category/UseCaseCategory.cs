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
        private readonly UserMiddlaware Middlaware;

#pragma warning disable IDE0290
        public UseCaseCategory(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
            Middlaware = new UserMiddlaware();
        }

        public string CriarCategoria(RequestCategory request)
        {
            var CategoryAlreadyExists = _context.Categories.FirstOrDefault(cat => cat.Name == request.Name);
            if (CategoryAlreadyExists != null)
            {
                throw new ExceptionCategory();
            }

            var loggedUser = new LoggedUserService(_httpContext);
            var user = loggedUser.GetUser(_context);

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
            var ActualLoggedUser = loggedUser.GetUser(_context);

            var categorieToBeRemoved = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            Middlaware.NullMid(categorieToBeRemoved, "Produto");
            Middlaware.UserMid(ActualLoggedUser, categorieToBeRemoved);

            _context.Categories.Remove(categorieToBeRemoved!);
            _context.SaveChanges();
        }

        public void UpdateCategory(RequestUpdateCategory request)
        {
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.GetUser(_context);

            var categorieToBeUpdate = _context.Categories.FirstOrDefault(c => c.Id == request.CategoryId);

            Middlaware.NullMid(categorieToBeUpdate, "Produto");
            Middlaware.UserMid(ActualLoggedUser, categorieToBeUpdate);

            categorieToBeUpdate!.Name = request.NewName;
            _context.SaveChanges();
        }

        public ResponseGetCategories GetCategory()
        {
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.GetUser(_context);

            Middlaware.UserMid<IOwnedUserId>(ActualLoggedUser, null);

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
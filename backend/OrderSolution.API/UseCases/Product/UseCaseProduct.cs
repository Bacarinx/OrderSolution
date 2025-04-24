using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation.Results;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Middleware;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.API.Validations;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.Product
{
    public class UseCaseProduct
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpContext;

#pragma warning disable IDE0290
        public UseCaseProduct(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public ResponseProduct CriarProduto(RequestNewProduct request)
        {
            var validator = new ValidationProductRegister();
            var responseValidation = validator.Validate(request);

            var categoryExists = _context.Categories.FirstOrDefault(cat => cat.Id == request.CategoryId);

            if (categoryExists == null)
            {
                responseValidation.Errors.Add(new ValidationFailure("CategoryId", "Essa categoria não existe, adicione o produto em uma categoria existente"));
            }

            if (request.Price <= 0)
                responseValidation.Errors.Add(new ValidationFailure("Price", "O preço precisa ser um valor positivo!"));

            if (!responseValidation.IsValid)
            {
                var errors = responseValidation.Errors.Select(error => error.ErrorMessage).ToList();
                var errormensages = new ExceptionRegisterUserResponse();
                var errorList = errormensages.Errors = errors;
                throw new ExceptionUserRegister(errorList);
            }
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.getUser(_context);

            _context.Products.Add(new Entities.Product
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price,
                UserId = ActualLoggedUser.Id
            });

            _context.SaveChanges();

            return new ResponseProduct
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price
            };
        }

        public List<ResponseProduct> ListarProdutosPorCategoria()
        {
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.getUser(_context);

            var query = _context.Products.AsQueryable();
            var result = query.Where(q => q.UserId == ActualLoggedUser.Id).OrderBy(product => product.Name);

            List<ResponseProduct> produtos = result.Select(produto => new ResponseProduct
            {
                Name = produto.Name,
                CategoryId = produto.CategoryId,
                Price = produto.Price
            }).ToList();

            if (produtos.Count == 0)
                throw new ExceptionProductsNotFound();

            return produtos;
        }

        public void RemoverProduto(int ProductId)
        {
            var loggedUser = new LoggedUserService(_httpContext);
            var ActualLoggedUser = loggedUser.getUser(_context);

            var nullMiddlaware = new NullMiddlaware();
            var userMiddlaware = new UserMiddlaware();

            var productToBeremoved = _context.Products.FirstOrDefault(prod => prod.Id == ProductId);

            nullMiddlaware.Execute(productToBeremoved, "Produto");
            userMiddlaware.Execute(ActualLoggedUser, productToBeremoved);

            _context.Products.Remove(productToBeremoved);
            _context.SaveChanges();
        }
    }
}
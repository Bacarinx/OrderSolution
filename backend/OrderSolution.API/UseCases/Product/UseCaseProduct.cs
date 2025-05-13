using FluentValidation.Results;
using OrderSolution.API.Context;
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
        private readonly Entities.User User;
        private readonly UserMiddlaware Middlaware;
        public UseCaseProduct(OrderSolutionDbContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;

            var loggedUser = new LoggedUserService(_httpContext);
            User = loggedUser.GetUser(_context);
            Middlaware = new UserMiddlaware();
        }


        public ResponseProduct CriarProduto(RequestNewProduct request)
        {
            var validator = new ValidationProductRegister();
            var responseValidation = validator.Validate(request);
            var categoryExists = _context.Categories.FirstOrDefault(cat => cat.Id == request.CategoryId);

            Middlaware.NullMid(categoryExists, "Token");

            if (request.Price <= 0)
                responseValidation.Errors.Add(new ValidationFailure("Price", "O preço precisa ser um valor positivo!"));

            if (!responseValidation.IsValid)
            {
                var errors = responseValidation.Errors.Select(error => error.ErrorMessage).ToList();
                var errormensages = new ExceptionRegisterUserResponse();
                var errorList = errormensages.Errors = errors;
                throw new ExceptionUserRegister(errorList);
            }

            _context.Products.Add(new Entities.Product
            {
                Name = request.Name,
                CategoryId = request.CategoryId,
                Price = request.Price,
                UserId = User.Id
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
            var query = _context.Products.AsQueryable();
            var result = query.Where(q => q.UserId == User.Id).OrderBy(product => product.Name);

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
            var productToBeremoved = _context.Products.FirstOrDefault(prod => prod.Id == ProductId);

            Middlaware.NullMid(productToBeremoved, "Produto");
            Middlaware.UserMid(User, productToBeremoved);

            _context.Products.Remove(productToBeremoved!);
            _context.SaveChanges();
        }

        public void AtualizarProduto(int ProductId, RequestProductUpdate request)
        {
            var productToBeUpdate = _context.Products.FirstOrDefault(prod => prod.Id == ProductId);

            Middlaware.NullMid(productToBeUpdate, "Produto");
            Middlaware.UserMid(User, productToBeUpdate);

            if (!String.IsNullOrEmpty(request.Name)) productToBeUpdate!.Name = request.Name;
            if (request.Price.HasValue) productToBeUpdate!.Price = request.Price.Value;
            if (request.Active.HasValue) productToBeUpdate!.Active = request.Active.Value;
        }
    }
}
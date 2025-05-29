using FluentValidation.Results;
using OrderSolution.API.Context;
using OrderSolution.API.Middleware;
using OrderSolution.API.Services.LoggedUser;
using OrderSolution.API.Validations;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.Client
{
    public class UseCaseClient
    {
        private readonly OrderSolutionDbContext _context;
        private readonly IHttpContextAccessor _httpcontext;
        private readonly UserMiddlaware _middlaware;
        private readonly Entities.User _user;
        public UseCaseClient(OrderSolutionDbContext context, IHttpContextAccessor httpcontext)
        {
            _context = context;
            _httpcontext = httpcontext;

            var loggedUser = new LoggedUserService(_httpcontext);
            _user = loggedUser.GetUser(_context);
            _middlaware = new UserMiddlaware();
        }

        public ResponseNewClient AddClient(RequestNewClient req)
        {
            var validator = new ValidationCPF();
            var responsevalidator = validator.Validate(req);

            var clientAlreadyExists = _context.Clients.FirstOrDefault(c => c.CPF == req.CPF);

            if (clientAlreadyExists != null)
            {
                responsevalidator.Errors.Add(new ValidationFailure("CPF Existente", "Já existe outra pessoa cadastrada com esse CPF"));
            }

            if (!responsevalidator.IsValid)
                throw new ExceptionUserRegister(responsevalidator.Errors.Select(e => e.ErrorMessage).ToList());

            var client = new Entities.Client
            {
                CPF = req.CPF,
                Email = req.Email,
                Gender = req.Gender,
                Name = req.Nome,
                PhoneNumber = req.PhoneNumber,
                UserId = _user.Id
            };

            _context.Clients.Add(client);
            _context.SaveChanges();

            var response = new ResponseNewClient()
            {
                Name = client.Name,
                CPF = client.CPF,
                Email = client.Email,
                Gender = client.Gender,
                PhoneNumber = client.PhoneNumber
            };

            return response;
        }

        public void RemoveClient(int ClientId)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == ClientId);
            _middlaware.NullMid(client, "Cliente");
            _middlaware.UserMid(_user, client);

            _context.Clients.Remove(client!);
            _context.SaveChanges();
        }

        public RequestUpdateClient UpdateClient(int ClientId, RequestUpdateClient req)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == ClientId);
            _middlaware.NullMid(client, "Cliente");
            _middlaware.UserMid(_user, client);

            var validator = new UpdateClientValidator();
            var responseValidator = validator.Validate(req);

            if (!responseValidator.IsValid)
                throw new ExceptionUserRegister(responseValidator.Errors.Select(e => e.ErrorMessage).ToList());

            if (!String.IsNullOrWhiteSpace(req.Email))
                client!.Email = req.Email;
            if (!String.IsNullOrWhiteSpace(req.Gender))
                client!.Gender = req.Gender;
            if (!String.IsNullOrWhiteSpace(req.Name))
                client!.Name = req.Name;
            if (!String.IsNullOrWhiteSpace(req.PhoneNumber))
                client!.PhoneNumber = req.PhoneNumber;

            _context.SaveChanges();

            return req;
        }
    }
}
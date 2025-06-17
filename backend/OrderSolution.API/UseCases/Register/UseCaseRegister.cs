using FluentValidation.Results;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Secutiry.Criptograph;
using OrderSolution.API.Secutiry.Token;
using OrderSolution.API.Validations;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.User
{
    public class UseCaseRegister
    {
        public ResponseUserRegisterJson Execute(RequestUserRegisterJson request, OrderSolutionDbContext context)
        {
            var validator = new ValidationUserRegister();
            var responseValidation = validator.Validate(request);

            var EmailAlreadyExists = context.Users.Count(user => user.Email == request.Email);
            var CnpjAlreadyExists = context.Users.Count(user => user.CNPJ == request.CNPJ);

            if (EmailAlreadyExists != 0)
                responseValidation.Errors.Add(new ValidationFailure("Email", "Email já cadastrado!"));

            if (CnpjAlreadyExists != 0)
                responseValidation.Errors.Add(new ValidationFailure("Cnpj", "CNPJ já cadastrado!"));

            if (!responseValidation.IsValid)
            {
                var errors = responseValidation.Errors.Select(error => error.ErrorMessage).ToList();

                var responseException = new ExceptionRegisterUserResponse();

                var errorList = responseException.Errors = errors;

                ; throw new ExceptionUserRegister(errorList);
            }

            var cript = new BCryptCriptograph();

            var newUser = new Entities.User
            {
                Name = request.Name,
                Email = request.Email,
                Password = cript.Criptographor(request.Password),
                CNPJ = request.CNPJ,
                Address = request.Address
            };

            context.Users.Add(newUser);

            context.SaveChanges();

            var tokenGenerator = new JwtToken();

            return new ResponseUserRegisterJson
            {
                Name = request.Name,
                AccessToken = tokenGenerator.Generate(newUser)
            };
        }
    }
}

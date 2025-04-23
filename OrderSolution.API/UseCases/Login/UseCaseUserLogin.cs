using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Secutiry.Criptograph;
using OrderSolution.API.Secutiry.Token;
using OrderSolution.Comunication.Requests;
using OrderSolution.Comunication.Responses;
using OrderSolutions.Exception;

namespace OrderSolution.API.UseCases.Login
{
    public class UseCaseUserLogin
    {
        public ResponseUserRegisterJson Execute(OrderSolutionDbContext context, RequestUserLoginJson request)
        {
            var Bcrypt = new BCryptCriptograph();
            var token = new JwtToken();

            var UserDb = context.Users.FirstOrDefault(user => user.Email == request.Email);
            if (UserDb == null || !Bcrypt.Verify(request.Password, UserDb))
            {
                throw new ExceptionLoginEmailNotFound();
            }

            var response = new ResponseUserRegisterJson
            {
                Name = UserDb.Name,
                AccessToken = token.Generate(UserDb)
            };

            return response;
        }
    }
}
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Context;
using OrderSolution.API.Entities;
using OrderSolution.API.Middleware;
using OrderSolutions.Exception;

namespace OrderSolution.API.Services.LoggedUser
{
    public class LoggedUserService
    {
        private readonly IHttpContextAccessor _context;

        public LoggedUserService(IHttpContextAccessor context)
        {
            _context = context;
        }

        public User GetUser(OrderSolutionDbContext dbContext)
        {
            var jwtToken = _context.HttpContext?.Request.Headers.Authorization.ToString();
            var middlaware = new UserMiddlaware();
            middlaware.NullMid(jwtToken, "Token");

            jwtToken = jwtToken![6..].Trim();
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            var stringId = token.Claims.First(claim => claim.Type == "Id").Value;
            var UserID = Convert.ToInt32(stringId);

            return dbContext.Users.First(user => user.Id == UserID);
        }
    }
}
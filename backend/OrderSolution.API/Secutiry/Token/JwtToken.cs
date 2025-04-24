using Microsoft.IdentityModel.Tokens;
using OrderSolution.API.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OrderSolution.API.Secutiry.Token
{
    public class JwtToken
    {
        public string Generate(User user)
        {
            var claims = new List<Claim>()
            {
                new Claim("Id", user.Id.ToString())
            };

            var Symmetric = new SymetricGenerator();

            var symetric = new SecurityTokenDescriptor()
            {
                Expires = DateTime.UtcNow.AddDays(2),
                SigningCredentials = new SigningCredentials(Symmetric.GetCredentials(), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(claims)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(symetric);
            return tokenHandler.WriteToken(token);
        }
    }
}

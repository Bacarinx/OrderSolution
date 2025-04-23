using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OrderSolution.API.Secutiry.Token
{
    public class SymetricGenerator
    {
        public SymmetricSecurityKey GetCredentials()
        {
            var key = "jkS89d2oASD21akolsdsAW1208AOSJNDw31982ajsdc210asojd2981";
            var securityKey = Encoding.UTF8.GetBytes(key);
            return new SymmetricSecurityKey(securityKey);
        }
    }
}

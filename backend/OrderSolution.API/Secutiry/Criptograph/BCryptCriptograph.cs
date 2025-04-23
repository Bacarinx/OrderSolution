using OrderSolution.API.Entities;

namespace OrderSolution.API.Secutiry.Criptograph
{
    public class BCryptCriptograph
    {
        public string Criptographor(string password)
        {
            var CriptografedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return CriptografedPassword;
        }

         public Boolean Verify(string password, User user)
        {
            var isCorrect = BCrypt.Net.BCrypt.Verify(password, user.Password);
            return isCorrect;
        }
        
    }
}

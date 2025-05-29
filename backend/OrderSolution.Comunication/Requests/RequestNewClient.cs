using System.ComponentModel.DataAnnotations;

namespace OrderSolution.Comunication.Requests
{
    public class RequestNewClient
    {
        public string CPF { get; set; } = String.Empty;
        public string Nome { get; set; } = String.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Gender { get; set; } = String.Empty;
    }
}
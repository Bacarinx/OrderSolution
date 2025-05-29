using System.ComponentModel.DataAnnotations.Schema;
using OrderSolution.API.Interfaces;

namespace OrderSolution.API.Entities
{
    public class Client : IOwnedUserId
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string CPF { get; set; } = String.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Gender { get; set; } = String.Empty;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}

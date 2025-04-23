using System.ComponentModel.DataAnnotations.Schema;

namespace OrderSolution.API.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public string CPF { get; set; } = String.Empty;
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}

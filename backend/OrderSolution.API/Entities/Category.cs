using System.ComponentModel.DataAnnotations.Schema;

namespace OrderSolution.API.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace OrderSolution.API.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = String.Empty;
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public decimal Price { get; set; }
        public bool Active { get; set; } = true;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("CategoryId")]
        public Category Category { get; set; } = null!;
    }
}

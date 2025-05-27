using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.API.Entities
{
    public class TabProducts
    {
        public int Id { get; set; }
        public int TabId { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime InsertionDate { get; set; }
        public bool IsPaid { get; set; }


        [ForeignKey("TabId")]
        public Tab Tab { get; set; } = null!;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
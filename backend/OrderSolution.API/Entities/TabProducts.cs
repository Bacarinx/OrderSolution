using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Interfaces;

namespace OrderSolution.API.Entities
{
    public class TabProducts : IOwnedUserId
    {
        public int Id { get; set; }
        public int TabId { get; set; }
        public int UserId { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public bool IsActive { get; set; }
        public int ServiceId { get; set; }
        public decimal Price { get; set; }
        public DateTime InsertionDate { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? PaymentDate { get; set; }

        [ForeignKey("TabId")]
        public Tab Tab { get; set; } = null!;

        [ForeignKey("ServiceId")]
        public Service Service { get; set; } = null!;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;
    }
}
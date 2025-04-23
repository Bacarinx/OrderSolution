using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.API.Entities
{
    public class ServiceClientProducts
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ServiceClientId { get; set; }
        public int ServiceId { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; } = null!;

        [ForeignKey("ServiceId")]
        public Service Service { get; set; } = null!;

        [ForeignKey("ServiceClientId")]
        public ServiceClient ServiceClient { get; set; } = null!;

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
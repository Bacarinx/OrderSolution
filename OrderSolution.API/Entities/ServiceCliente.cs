using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.API.Entities
{
    public class ServiceClient
    {
        public int Id { get; set; }
        public int ServiceId { get; set; }
        public int ClientId { get; set; }

        [ForeignKey("ServiceId")]
        public Service Service { get; set; } = null!;

        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;

        public int UserId{ get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
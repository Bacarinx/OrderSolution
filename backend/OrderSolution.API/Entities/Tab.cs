using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using OrderSolution.API.Interfaces;

namespace OrderSolution.API.Entities
{
    public class Tab : IOwnedUserId
    {
        public int Id { get; set; }
        public string Code { get; set; } = String.Empty;
        public int UserId { get; set; }
        public int? ClientId { get; set; }
        public bool? IsOpen { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [ForeignKey("ClientId")]
        public Client Client { get; set; } = null!;

    }
}
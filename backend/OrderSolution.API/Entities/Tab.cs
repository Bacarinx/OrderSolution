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
        public int UserId { get; set; }
        public bool IsActive { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
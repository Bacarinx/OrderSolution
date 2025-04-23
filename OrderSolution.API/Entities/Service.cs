using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.API.Entities
{
    public class Service
    {
        public int Id { get; set; }
        public DateTime StartService { get; set; } = DateTime.UtcNow;
        public DateTime? EndService { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;
    }
}
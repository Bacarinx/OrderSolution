using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.API.Interfaces
{
    public interface IOwnedUserId
    {
        public int UserId { get; set; }
    }
}
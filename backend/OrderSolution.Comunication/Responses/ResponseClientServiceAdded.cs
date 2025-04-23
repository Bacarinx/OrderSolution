using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseClientServiceAdded
    {
        public int UserId { get; set; }
        public int ServiceId { get; set; }
    }
}
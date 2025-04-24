using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Requests
{
    public class RequestUpdateCategory
    {
        public int CategoryId { get; set; }
        public string NewName { get; set; } = String.Empty;
    }
}
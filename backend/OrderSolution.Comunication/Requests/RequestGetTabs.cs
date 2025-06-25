using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Requests
{
    public class RequestGetTabs
    {
        public int PageNumber { get; set; }
        public string Code { get; set; } = String.Empty;
    }
}
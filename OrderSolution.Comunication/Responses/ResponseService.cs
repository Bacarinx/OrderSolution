using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseService
    {
        public DateTime StartService { get; set; }
        public DateTime EndService { get; set; }
        public int UserId { get; set; }
    }
}
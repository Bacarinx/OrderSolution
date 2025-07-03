using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseGetOneService
    {
        public int ServiceId { get; set; }
        public DateTime StartService { get; set; }
        public DateTime? EndService { get; set; }
        public List<ResponseClient> Clients { get; set; } = [];
        public decimal Value { get; set; }
    }
}
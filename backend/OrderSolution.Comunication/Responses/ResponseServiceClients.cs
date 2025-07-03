using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseServiceClients
    {
        public int ServiceId { get; set; }
        public List<ResponseClient> Clients { get; set; } = [];
    }
}
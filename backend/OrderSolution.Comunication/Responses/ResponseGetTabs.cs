using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ResponseGetTabs
    {
        public List<ResponseTab> Tabs { get; set; } = [];
        public int Qtd { get; set; }

    }
}
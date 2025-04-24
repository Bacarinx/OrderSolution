using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderSolution.Comunication.Responses
{
    public class ModelCategory
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = String.Empty;
    }

    public class ResponseGetCategories
    {
        public List<ModelCategory> Categories { get; set; } = [];
    }
}
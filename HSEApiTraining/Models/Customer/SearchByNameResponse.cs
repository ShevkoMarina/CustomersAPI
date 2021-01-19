using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSEApiTraining.Models.Customer
{
    public class SearchByNameResponse
    {
        public IEnumerable<Customer> Customers { get; set; }
        public string Error { get; set; }
    }
}

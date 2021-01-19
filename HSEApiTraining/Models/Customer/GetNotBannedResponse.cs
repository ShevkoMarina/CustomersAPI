using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSEApiTraining.Models.Customer
{
    public class GetNotBannedResponse
    {
        public IEnumerable<Customer> NotBannedCustomers { get; set; }

        public string Error { get; set; }
    }
}

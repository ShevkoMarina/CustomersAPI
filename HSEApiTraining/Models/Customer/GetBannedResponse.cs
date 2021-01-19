using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSEApiTraining.Models.Customer
{
    public class GetBannedResponse
    {
        public IEnumerable<Customer> BannedCustomers { get; set; }

        public string Error { get; set; }
    }
}

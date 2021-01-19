using System.Collections.Generic;

namespace HSEApiTraining.Models.Customer
{
    public class GetCustomersResponse
    {
        public IEnumerable<Customer> Customers { get; set; }

        public string Error { get; set; }
    }
}

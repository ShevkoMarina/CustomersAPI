using System.Collections.Generic;
using HSEApiTraining.Models.Customer;

namespace HSEApiTraining
{
    public interface ICustomerService
    {
        (IEnumerable<Customer> Customers, string Error) GetCustomers(int count);

        (IEnumerable<Customer> Customers, string Error) GetAllCustomers();

        (IEnumerable<Customer> Customers, string Error) SearchByName(string searchItem);

        (IEnumerable<Customer> Customers, string Error) SearchBySurname(string searchItem);

        (IEnumerable<Customer> BannedCustomers, string Error) GetBannedCustomers();

        (IEnumerable<Customer> NotBannedCustomers, string Error) GetNotBannedCustomers();

        string GenerateRandomCustomers(GenerateRandomCustomersRequest request);

        string AddCustomer(AddCustomerRequest request);

        string UpdateCustomer(int id, UpdateCustomerRequest request);

        string DeleteCustomer(int id);

        string DeleteAllCustomers();
    }
}

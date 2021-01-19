using HSEApiTraining.Models.Customer;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public interface ICustomerRepository
    {
        (IEnumerable<Customer> Customers, string Error) GetCustomers(int count);

        string AddCustomer(AddCustomerRequest request);

        string DeleteCustomer(int id);

        string UpdateCustomer(int id, UpdateCustomerRequest request);

        (IEnumerable<Customer> Customers, string Error) GetAllCustomers();

        (IEnumerable<Customer> Customers, string Error) SearchByName(string searchItem);

        (IEnumerable<Customer> Customers, string Error) SearchBySurname(string searchItem);

        (IEnumerable<Customer> BannedCustomers, string Error) GetBannedCustomers();

        (IEnumerable<Customer> NotBannedCustomers, string Error) GetNotBannedCustomers();

        string DeleteAllCustomers();
    }
}

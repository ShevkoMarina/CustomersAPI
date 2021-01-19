using System.Collections.Generic;
using System.Linq;
using System;
using HSEApiTraining.Models.Customer;
using System.Text.RegularExpressions;

namespace HSEApiTraining.Services
{
    public class CustomerService : ICustomerService
    {
        private static Random random = new Random();
        private ICustomerRepository _customerRepository;
        private readonly string[] phone_codes = { "+7", "+0", "+380", "+1" };
        private readonly string[] names = {"Anna", "Adam", "Alisa", "Bob", "Ben",
                                           "Den", "Jack", "Tom", "Sarah", "Nick"};
        private readonly string[] surnames = {"Smith", "Johnson", "Hall", "Allen", "Wood",
                                              "Morris", "Ross", "King", "Brown", "Reed"};

        public CustomerService(ICustomerRepository customerRepository)
            => _customerRepository = customerRepository;

        public (IEnumerable<Customer> Customers, string Error) GetCustomers(int count)
        {
            if (count < 0)
            {
                return (null, "Number of customers can't be negative");
            }
            return _customerRepository.GetCustomers(count);
        }

        public (IEnumerable<Customer> Customers, string Error) GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public string GenerateRandomCustomers(GenerateRandomCustomersRequest request)
        {
            string phoneNumber, name, surname;
            List<string> Errors = new List<string>();

            if (request.Count < 0)
                return "Number of customers can't be negative";
            for (int i = 0; i < request.Count; i++)
            {
                phoneNumber = phone_codes[random.Next(4)];
                for (int j = 0; j < 8; j++)
                {
                    phoneNumber += random.Next(10);
                }
                name = names[random.Next(10)];
                surname = surnames[random.Next(10)];
                string error = AddCustomer(new AddCustomerRequest
                {
                    Name = name,
                    Surname = surname,
                    PhoneNumber = phoneNumber
                });
                Errors.Add(error);
            }
            return  Errors.Where(e => e != null)?.FirstOrDefault();
        }

        public string AddCustomer(AddCustomerRequest request)
        {
            if (String.IsNullOrEmpty(request.Name))
                return "Name can't be empty or null";

            if (!Regex.IsMatch(request.PhoneNumber, @"^\+[0-9]{1,12}$"))
                return $"Phone '{request.PhoneNumber}' has wrong format";

            return _customerRepository.AddCustomer(request);
        }

        public string DeleteCustomer(int id)
            => _customerRepository.DeleteCustomer(id);

        public string UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            if (string.IsNullOrEmpty(request.Name))
                return "Name cannot be null or empty";

            if (!Regex.IsMatch(request.PhoneNumber, @"^\+[0-9]{1,12}$"))
                return $"Phone '{request.PhoneNumber}' has wrong format";

            return _customerRepository.UpdateCustomer(id, request);
        }

        public string DeleteAllCustomers()
            => _customerRepository.DeleteAllCustomers();

        public (IEnumerable<Customer> Customers, string Error) SearchByName(string searchItem)
        {
            if (string.IsNullOrEmpty(searchItem))
                return (null, "searchTerm cannot be empty");

            return _customerRepository.SearchByName(searchItem);
        }

        public (IEnumerable<Customer> Customers, string Error) SearchBySurname(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))

                return (null, "searchTerm cannot be empty");

            return _customerRepository.SearchBySurname(searchTerm);
        }

        public (IEnumerable<Customer> BannedCustomers, string Error) GetBannedCustomers()
            => _customerRepository.GetBannedCustomers();

        public (IEnumerable<Customer> NotBannedCustomers, string Error) GetNotBannedCustomers()
            => _customerRepository.GetNotBannedCustomers();
    }
}

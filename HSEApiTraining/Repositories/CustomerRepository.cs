using Dapper;
using HSEApiTraining.Models.Customer;
using HSEApiTraining.Providers;
using System;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ISQLiteConnectionProvider _connectionProvider;

        public CustomerRepository(ISQLiteConnectionProvider sqliteConnectionProvider)
        {
            _connectionProvider = sqliteConnectionProvider;
        }

        public (IEnumerable<Customer> Customers, string Error) GetAllCustomers()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<Customer>(@"
                        SELECT
                        id as Id,
                        name as Name, 
                        surname as Surname, 
                        phone_number as PhoneNumber 
                        FROM Customer"),
                        null);
                }
            }
            catch(Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> Customers, string Error) GetCustomers(int count)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (
                        connection.Query<Customer>(@"
                        SELECT 
                        id as Id,
                        name as Name, 
                        surname as Surname, 
                        phone_number as PhoneNumber 
                        FROM Customer 
                        LIMIT @count", 
                        new {count = count}), 
                        null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public string AddCustomer(AddCustomerRequest request)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    connection.Execute(
                        @"INSERT INTO Customer 
                        ( name, surname, phone_number ) VALUES 
                        ( @Name, @Surname, @PhoneNumber );", 
                        new {Name = request.Name, Surname = request.Surname, PhoneNumber = request.PhoneNumber });
                }
                return null;
            }
            catch(Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteCustomer(int id)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    int affectedRows = connection.Execute(
                        @"DELETE FROM Customer WHERE id = @Id;",
                        new { Id = id });
                   
                    if (affectedRows == 0) return "Nothing to delete";
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string DeleteAllCustomers()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    connection.Execute(
                        @"DELETE FROM Customer");
                    connection.Execute(@"DELETE FROM sqlite_sequence WHERE name = 'customer'");

                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public string UpdateCustomer(int id, UpdateCustomerRequest request)
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    int affectedRows = connection.Execute(
                        @"UPDATE Customer 
                        SET name = @Name, surname = @Surname, phone_number = @PhoneNumber
                        WHERE id = @Id;",
                        new { Name = request.Name, Surname = request.Surname, PhoneNumber = request.PhoneNumber, Id = id });
                    if (affectedRows == 0) return "Nothing to edit";
                }
                return null;
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public (IEnumerable<Customer>, string) SearchByName(string searchTerm)
        {
            try
            {
                searchTerm = $"%{searchTerm}%";
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (connection.Query<Customer>(@"
                         SELECT 
                         id as Id,
                         name as Name, 
                         surname as Surname, 
                         phone_number as PhoneNumber 
                         FROM Customer 
                         WHERE name LIKE @searchTerm", new { searchTerm = searchTerm }), null); 
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer>, string) SearchBySurname(string searchTerm)
        {
            try
            {
                searchTerm = $"%{searchTerm}%";
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (connection.Query<Customer>(@"
                         SELECT 
                         id as Id,
                         name as Name, 
                         surname as Surname, 
                         phone_number as PhoneNumber 
                         FROM Customer 
                         WHERE surname LIKE @searchTerm", new { searchTerm = searchTerm }), null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> BannedCustomers, string Error) GetBannedCustomers()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();
                    return (connection.Query<Customer>(@"
                         SELECT
                         customer.id as Id,
                         name as Name, 
                         surname as Surname, 
                         phone_number as PhoneNumber 
                         FROM customer, banned_phone 
                         WHERE customer.phone_number = banned_phone.phone"), null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }

        public (IEnumerable<Customer> NotBannedCustomers, string Error) GetNotBannedCustomers()
        {
            try
            {
                using (var connection = _connectionProvider.GetDbConnection())
                {
                    connection.Open();

                    return (connection.Query<Customer>(@"
                         SELECT DISTINCT
                         customer.id as Id,
                         name as Name, 
                         surname as Surname, 
                         phone_number as PhoneNumber 
                         FROM customer, banned_phone  
                         WHERE customer.phone_number NOT IN (
                         SELECT phone
                         FROM banned_phone)"), null);
                }
            }
            catch (Exception e)
            {
                return (null, e.Message);
            }
        }
    }
}

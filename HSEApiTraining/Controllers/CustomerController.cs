using HSEApiTraining.Models.Customer;
using Microsoft.AspNetCore.Mvc;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public GetCustomersResponse GetCustomers([FromQuery]int count)
        {
            var result = _customerService.GetCustomers(count);
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }

        [HttpGet("GetAll")]
        public GetCustomersResponse GetAllCustomers()
        {
            var result = _customerService.GetAllCustomers();
            return new GetCustomersResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }

        [HttpPost]
        public AddCustomerResponse AddCustomer([FromBody] AddCustomerRequest request)
        {
            return new AddCustomerResponse
            {
                Error = _customerService.AddCustomer(request)
            };
        }

        [HttpPut("{id}")]
        public UpdateCustomerResponse UpdateCustomer(int id, [FromBody] UpdateCustomerRequest request)
        {
            return new UpdateCustomerResponse
            {
                Error = _customerService.UpdateCustomer(id, request)
            };
        }

        [HttpDelete("{id}")]
        public DeleteCustomerResponse DeleteCustomer(int id)
        {
            return new DeleteCustomerResponse
            {
                Error = _customerService.DeleteCustomer(id)
            };
        }

        [HttpPost("GenerateRandomCustomers")]
        public GenerateRandomCustomersResponse GenerateRandomCustomers([FromBody] GenerateRandomCustomersRequest request)
        {
            var result = _customerService.GenerateRandomCustomers(request);
            return new GenerateRandomCustomersResponse
            {
                Error = result
            };
        }


        [HttpDelete("DeleteAll")]
        public DeleteAllResponse DeleteAll()
        {
            return new DeleteAllResponse
            {
                Error = _customerService.DeleteAllCustomers()
            };

        }

        [HttpGet("SearchByName/{searchTerm}")]
        public SearchByNameResponse SearchByName(string searchTerm)
        {
            var result = _customerService.SearchByName(searchTerm);
            return new SearchByNameResponse
            {
                Customers = result.Customers,
                Error = result.Error
            };
        }

        [HttpGet("SearchBySurname/{searchTerm}")]
        public SearchBySurnameResponse SearchBySurname(string searchTerm)
        {
            var result = _customerService.SearchBySurname(searchTerm);
            return new SearchBySurnameResponse
            {
                Customers = result.Customers,
            };
        }

        [HttpGet("getBanned")]
        public GetBannedResponse GetBanned()
        {
            var result = _customerService.GetBannedCustomers();
            return new GetBannedResponse
            {
                BannedCustomers = result.BannedCustomers,
                Error = result.Error
            };
        }

        [HttpGet("getNotBanned")]
        public GetNotBannedResponse GetNotBanned()
        {
            var result = _customerService.GetNotBannedCustomers();
            return new GetNotBannedResponse
            {
                NotBannedCustomers = result.NotBannedCustomers,
                Error = result.Error
            };
        }
    }
}

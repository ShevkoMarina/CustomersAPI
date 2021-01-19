using HSEApiTraining.Models.Ban;
using HSEApiTraining.Services;
using Microsoft.AspNetCore.Mvc;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanController : Controller
    {
        private readonly IBanService _banService;

        public BanController(IBanService banService)
        {
            _banService = banService;
        }

        [HttpGet]
        public GetBannedPhonesResponse Get()
        {
            var result = _banService.GetBannedPhones();
            return new GetBannedPhonesResponse
            {
                Phones = result.Phones,
                Error = result.Error
            };
        }

        [HttpPost]
        public AddBannedPhoneResponse Post([FromBody] AddBannedPhoneRequest request)
        {
            return new AddBannedPhoneResponse
            {
                Error = _banService.AddBannedPhone(request)
            };
        }

        [HttpDelete("{id}")]
        public DeleteBannedPhoneResponse Delete([FromRoute] int id)
        {
            return new DeleteBannedPhoneResponse
            {
                Error = _banService.DeletePhone(id)
            };
        }

        [HttpDelete("DeleteAll")]
        public DeleteAllBannedPhonesResponse Delete()
        {
            return new DeleteAllBannedPhonesResponse
            {
                Error = _banService.DeleteAll()
            };
        }
    }
}

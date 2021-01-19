using HSEApiTraining.Models.Ban;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HSEApiTraining.Services
{
    public class BanService : IBanService
    {
        private readonly IBanRepository _banPerository;

        public BanService(IBanRepository banRepository)
        {
            _banPerository = banRepository;
        }

        public string AddBannedPhone(AddBannedPhoneRequest request)
        {
            if (!Regex.IsMatch(request.Phone, @"^\+[0-9]{1,12}$"))
                return $"Phone '{request.Phone}' has wrong format";

            return _banPerository.AddBannedPhone(request.Phone);
        }

        public string DeleteAll()
            => _banPerository.DeleteAll();

        public string DeletePhone(int id) 
            => _banPerository.DeletePhone(id);

        public (string Error, IEnumerable<BannedPhone> Phones) GetBannedPhones()
            => _banPerository.GetBannedPhones();
    }
}

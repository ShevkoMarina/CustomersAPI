using HSEApiTraining.Models.Ban;
using System.Collections.Generic;

namespace HSEApiTraining.Services
{
    public interface IBanService
    {
        string AddBannedPhone(AddBannedPhoneRequest request);

        (string Error, IEnumerable<BannedPhone> Phones) GetBannedPhones();

        string DeletePhone(int id);

        string DeleteAll();
    }
}

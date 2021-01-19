using HSEApiTraining.Models.Ban;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public interface IBanRepository
    {
        string AddBannedPhone(string bannedPhones);

        (string Error, IEnumerable<BannedPhone> Phones) GetBannedPhones();

        string DeletePhone(int id);

        string DeleteAll();
    }
}

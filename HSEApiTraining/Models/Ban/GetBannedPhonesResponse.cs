using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSEApiTraining.Models.Ban
{
    public class GetBannedPhonesResponse
    {
        public IEnumerable<BannedPhone> Phones { get; set; }

        public string Error { get; set; }
    }
}

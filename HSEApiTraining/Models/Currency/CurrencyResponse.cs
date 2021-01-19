using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSEApiTraining.Models
{
    public class CurrencyResponse
    {
        public IEnumerable<double> CurrencyValues { get; set; }

        public string Error { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSEApiTraining.Models.Calculator
{
    public class CalculatorBatchResponse
    {
        public IEnumerable<CalculatorResponse> Values { get; set; }

        public string Error { get; set; }
    }
}

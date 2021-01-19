using System.Collections.Generic;

namespace HSEApiTraining.Models.Calculator
{
    public class CalculatorBatchRequest
    {
        public IEnumerable<string> Expressions { get; set; }
    }
}

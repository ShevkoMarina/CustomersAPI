using HSEApiTraining.Models.Calculator;
using System.Collections.Generic;

namespace HSEApiTraining
{
    public interface ICalculatorService
    {
        (string, double) CalculateExpression(string expression);

        (string, IEnumerable<CalculatorResponse>) CalculateBatchExpressions(IEnumerable<string> expressions);
    }
}

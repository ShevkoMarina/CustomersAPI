using HSEApiTraining.Models.Calculator;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace HSEApiTraining
{
    internal delegate double Calculate(double a, double b);

    public class CalculatorService : ICalculatorService
    {
        readonly static string separator = CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator;

        readonly Dictionary<string, Calculate> MathOperations = new Dictionary<string, Calculate>
        {
            { "--", (a, b) => a + b },
            { "-+", (a, b) => a - b },
            { "+-", (a, b) => a - b },
            { "*-", (a, b) => a * (-b) },
            { "/-", (a, b) => a / (-b) },
            { "%-", (a, b) => a % (-b) },
            { "*", (a, b) => a * b },
            { "/", (a, b) => a / b },
            { "%", (a, b) => a % b },
            { "+", (a, b) => a + b },
            { "-", (a, b) => a - b }
        };

        /// <summary>
        /// Calculates the batch of expressions.
        /// </summary>
        /// <param name="expressions"></param>
        /// <returns>Calculated expressions</returns>
        public (string, IEnumerable<CalculatorResponse>) CalculateBatchExpressions(IEnumerable<string> expressions)
        {
            string errorMessage = null;
            if (expressions.Count() == 0)
            {
                errorMessage = "Expressions list is empty";
                return (errorMessage, null);
            }

            List<CalculatorResponse> result = new List<CalculatorResponse>();

            foreach (string expression in expressions)
            {
                (string error, double value) = CalculateExpression(expression);

                if (error != null)
                {
                    errorMessage = "Wrong expression was found";
                }

                var response = new CalculatorResponse
                {
                    Value = value,
                    Error = error
                };

                result.Add(response);
            }

            return (errorMessage, result);
        }

        /// <summary>
        /// Calculates the expression
        /// </summary>
        /// <param name="expression"></param>
        /// <returns>Result of calculaton</returns>
        public (string, double) CalculateExpression(string expression)
        {
            string errorMessage = null;
            if (expression == null)
            {
                errorMessage = "Expression is empty";
                return (errorMessage, 0);
            }

            List<string> nums = new List<string>();
            Calculate calculate;

            expression = expression.Trim();
            expression = expression.Replace(",", separator);
            expression = expression.Replace(".", separator);
            int coef = 1;
            if (expression[0] == '-')
            {
                coef = -1;
                expression = expression[1..];
            }

            Regex regex = new Regex(@"\d*[.,]*\d*");
            foreach (var match in regex.Matches(expression))
            {
                if (match.ToString() != "")
                {
                    nums.Add(match.ToString());
                }
            }

            if (nums.Count != 2)
            {
                errorMessage = "Wrong count of numbers";
                return (errorMessage, 0);
            }

            string op = expression;
            for (int i = 0; i < nums.Count; i++)
            {
                op = op.Trim(nums[i].ToCharArray());
            }
            op = op.Replace(" ", "");

            foreach (string symb in MathOperations.Keys)
            {
                if (op == symb)
                {
                    calculate = MathOperations[symb];
                    try
                    {
                        double num1 = double.Parse(nums[0]) * coef;
                        double num2 = double.Parse(nums[1]);
                        var result = CalculateExpressionHelper(num1, num2, calculate);
                        return (errorMessage, result);
                    }
                    catch (FormatException)
                    {
                        errorMessage = "Wrong numbers format in expression";
                        return (errorMessage, 0);
                    }
                    catch (Exception ex)
                    {
                        return (ex.Message, 0);
                    }
                }
            }

            errorMessage = "Wrong symbol in expression";
            return (errorMessage, 0);
        }

        /// <summary>
        /// Parse string array to double numbers, calculates and trims result.
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="calculate"></param>
        /// <returns>Result in requred format</returns>
        private static double CalculateExpressionHelper(double num1, double num2, Calculate calculate)
        {
            if (num1 > int.MinValue && num1 < int.MaxValue
                && num2 > int.MinValue && num2 < int.MaxValue)
            {
                double calculatedRes = calculate(num1, num2);
                if (double.IsInfinity(calculatedRes))
                {
                    throw new DivideByZeroException("Division by zero is illegal");
                }
                int index = $"{calculatedRes}".IndexOf(separator);
                if (index == -1)
                {
                    return calculatedRes;
                }
                else
                {
                    try
                    {
                        double result = double.Parse($"{calculatedRes}".Substring(0, index + 5));
                        return result;
                    }
                    catch (Exception)
                    {
                        return calculatedRes;
                    }
                }
            }
            else
            {
                throw new ArgumentException("Numbers are not in bounds");
            }
        }
    }
}

using HSEApiTraining.Models.Calculator;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;

        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        /// <summary>
        /// Calculates expression
        /// </summary>
        /// <param name="expression">expression from query</param>
        /// <returns>calculated expression</returns>
        [HttpGet]
        public CalculatorResponse Calculate([FromQuery] string expression)
        {
            (string errorMessage, var result) = 
                _calculatorService.CalculateExpression(expression);
            return new CalculatorResponse
            {
                Value = result,
                Error = errorMessage 
            };
        }

        [HttpPost]
        public CalculatorBatchResponse CalculateBatch([FromBody] CalculatorBatchRequest Request)
        {
            (string errorMessage, var result) = 
                _calculatorService.CalculateBatchExpressions(Request.Expressions);
            return new CalculatorBatchResponse
            {
                Values = result,
                Error = errorMessage
            };
        }

        //Примеры-пустышки
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
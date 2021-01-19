using HSEApiTraining.Models;
using HSEApiTraining.Models.Currency;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService _currencyController;

        public CurrencyController(ICurrencyService currencyController)
        {
            _currencyController = currencyController;
        }

        [HttpGet]
        public IActionResult DummyMethod()
        {
            return View();
        }

        /// <summary>
        /// Gets currency values
        /// </summary>
        /// <param name="currencyRequest"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Currency([FromBody] CurrencyRequest currencyRequest)
        {
            (string errorMessage, IEnumerable<double> currencyValues) = 
                await _currencyController.GetCurrencyValues(currencyRequest);
            return Ok(new CurrencyResponse
            {
                Error =  errorMessage,
                CurrencyValues = currencyValues
            });
        }
    }
}

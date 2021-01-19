using HSEApiTraining.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HSEApiTraining.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : Controller
    {
        private readonly IDummyService _dummyService;

        public DummyController(IDummyService dummyService)
        {
            _dummyService = dummyService;
        }

        /// <summary>
        /// Generates random number from 0 to the number from route
        /// </summary>
        /// <param name="number">number from route</param>
        /// <returns>string with a generated number</returns>
        [HttpGet("generate/{number}")]
        public string DummyGenerator([FromRoute] int number)
        {
            return _dummyService.Generator(number);
        }
    }
}

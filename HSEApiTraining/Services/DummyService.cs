using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HSEApiTraining.Services
{
    public class DummyService : IDummyService
    {
        private static readonly Random rand = new Random();

        /// <summary>
        /// Generates random number
        /// </summary>
        /// <param name="number">number from route</param>
        /// <returns>string with a generated number</returns>
        public string Generator(int number)
        {
            return $"Random (0, {number}): {rand.Next(number)}";
        }
    }
}

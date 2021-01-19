using HSEApiTraining.Models.Currency;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HSEApiTraining.Controllers
{
    public interface ICurrencyService
    {
        public Task<(string, IEnumerable<double>)> GetCurrencyValues(CurrencyRequest currencyRequest);
    }
}
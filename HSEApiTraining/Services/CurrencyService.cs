using HSEApiTraining.Controllers;
using HSEApiTraining.Models.Currency;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace HSEApiTraining.Services
{
    public class CurrencyService : ICurrencyService
    {
        public static HttpClient client;
        public const string url = "https://api.ratesapi.io/api/";

        public async Task<(string, IEnumerable<double>)> GetCurrencyValues(CurrencyRequest currencyRequest)
        {
            string errorMessage = null;
            if (currencyRequest.Symbol == null || currencyRequest.Symbol == "")
            {
                errorMessage = "Symbol cannot be null";
                return (errorMessage, null);
            }
            client ??= new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            currencyRequest.DateStart ??= DateTime.Now;
            currencyRequest.DateEnd ??= currencyRequest.DateStart;
            List<double> CurrencyValues = new List<double>();

            while (currencyRequest.DateEnd >= currencyRequest.DateStart)
            {
                string date = currencyRequest.DateStart.Value.ToString("yyyy-MM-dd");
                HttpResponseMessage response = await client.GetAsync($"{date}?base={currencyRequest.Symbol}&symbols=RUB");

                if (response.IsSuccessStatusCode)
                {
                    string json = await response.Content.ReadAsStringAsync();
                    JObject jObj = JObject.Parse(json);
                    if (jObj["error"] != null)
                    {
                        errorMessage = $"{jObj["error"]}";
                        return (errorMessage, null);
                    }

                    var value = jObj["rates"];
                    CurrencyValues.Add(double.Parse($"{value["RUB"]}"));
                }
                else
                {
                    errorMessage = "Incorrect data was found in the request";
                    return (errorMessage, null);
                }

                currencyRequest.DateStart = currencyRequest.DateStart.Value.AddDays(1);
            }

            return (errorMessage, CurrencyValues);
        }
    }
}

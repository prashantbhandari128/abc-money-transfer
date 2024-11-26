using ABCMoneyTransfer.Service.Interface;
using ABCMoneyTransfer.Service.Result.ExchangeRate;
using System.Text.Json;

namespace ABCMoneyTransfer.Service.Implementation
{
    public class CurrentExchangeService : ICurrentExchangeService
    {
        private readonly HttpClient _httpClient;

        public CurrentExchangeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<EcxhangeRateResult.Rate>> GetExchangeRatesAsync()
        {
            var today = DateTime.Now;
            var apiUrl = $"https://www.nrb.org.np/api/forex/v1/rates?page=1&per_page=5&from={today.ToString("yyyy-MM-dd")}&to={today.ToString("yyyy-MM-dd")}";
            var response = await _httpClient.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<EcxhangeRateResult>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result?.data?.payload?[0]?.rates;
            }
            return new List<EcxhangeRateResult.Rate>(); // Return an empty list in case of failure
        }

        public async Task<decimal> GetMalaysiaSellRateAsync()
        {
            var rates = await GetExchangeRatesAsync();
            return rates.Where(x => x.currency.name == "Malaysian Ringgit").Select(x => Convert.ToDecimal(x.sell)).SingleOrDefault();
        }
    }
}

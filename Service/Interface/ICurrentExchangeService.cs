using ABCMoneyTransfer.Service.Result.ExchangeRate;

namespace ABCMoneyTransfer.Service.Interface
{
    public interface ICurrentExchangeService
    {
        Task<IEnumerable<EcxhangeRateResult.Rate>> GetExchangeRatesAsync();
        Task<decimal> GetMalaysiaSellRateAsync();
    }
}

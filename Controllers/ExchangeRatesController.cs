using ABCMoneyTransfer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    [Authorize]
    public class ExchangeRatesController : Controller
    {
        private readonly ICurrentExchangeService _currentExchangeService;

        public ExchangeRatesController(ICurrentExchangeService currentExchangeService)
        {
            _currentExchangeService = currentExchangeService;
        }

        public async Task<IActionResult> Index()
        {
            var rates = await _currentExchangeService.GetExchangeRatesAsync();
            return View(rates);
        }
    }
}

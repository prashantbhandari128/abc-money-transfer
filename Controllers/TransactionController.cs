using ABCMoneyTransfer.Helper.Data.Toastr;
using ABCMoneyTransfer.Helper.Interface;
using ABCMoneyTransfer.Persistence.Entities;
using ABCMoneyTransfer.Service.Interface;
using ABCMoneyTransfer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;
        private readonly IToastrHelper _toastrHelper;

        public TransactionController(ITransactionService transactionService, IToastrHelper toastrHelper)
        {
            _transactionService = transactionService;
            _toastrHelper = toastrHelper;
        }

        [HttpGet]
        public async Task<IActionResult> Index(DateTime? from, DateTime? to)
        {
            ViewBag.from = from?.ToString("yyyy-MM-dd");
            ViewBag.to = to?.ToString("yyyy-MM-dd");
            List<TransactionViewModel> transactions = new List<TransactionViewModel>();
            if (from == null && to == null)
            {
                transactions = await _transactionService.GetTransactionsAsync();
            }
            else
            {
                if (from.HasValue && to.HasValue && from > to)
                {
                    _toastrHelper.SendMessage(this, "ABC Money Transfer", "The from date cannot be greater than the to date.", MessageType.Warning);
                }
                else
                {
                    transactions = await _transactionService.GetTransactionsAsync(from, to);
                }
            }
            return View(transactions);
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            Transaction? transaction = await _transactionService.GetTransactionAsync(id);
            return View(transaction);
        }
    }
}

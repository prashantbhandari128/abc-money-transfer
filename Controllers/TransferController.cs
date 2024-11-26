using ABCMoneyTransfer.DTO;
using ABCMoneyTransfer.Helper.Data.Toastr;
using ABCMoneyTransfer.Helper.Interface;
using ABCMoneyTransfer.Service.Implementation;
using ABCMoneyTransfer.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABCMoneyTransfer.Controllers
{
    [Authorize]
    public class TransferController : Controller
    {
        private readonly ITransferService _transferService;
        private readonly IToastrHelper _toastrHelper;

        public TransferController(ITransferService transferService, IToastrHelper toastrHelper)
        {
            _transferService = transferService;
            _toastrHelper = toastrHelper;
        }

        [HttpGet]
        public IActionResult Send()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Send(TransferDto transferDto)
        {
            if (!ModelState.IsValid)
            {
                return View(transferDto);
            }
            var result = await _transferService.TransferAsync(transferDto);
            if (result.Status == true)
            {
                _toastrHelper.SendMessage(this, "ABC Money Transfer", result.Message, MessageType.Success);
                return RedirectToAction("View", "Transaction", new { id = result.Data.TrasactionId });
            }
            _toastrHelper.SendMessage(this, "ABC Money Transfer", result.Message, MessageType.Error);
            ModelState.AddModelError("", result.Message);
            return View(transferDto);
        }
    }
}

using ABCMoneyTransfer.DTO;
using ABCMoneyTransfer.Persistence.Entities;
using ABCMoneyTransfer.Persistence.UnitOfWork.Interface;
using ABCMoneyTransfer.Service.Interface;
using ABCMoneyTransfer.Service.Result;
using AutoMapper;

namespace ABCMoneyTransfer.Service.Implementation
{
    public class TransferService : ITransferService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentExchangeService _currentExchangeService;

        public TransferService(IUnitOfWork unitOfWork, IMapper mapper, ICurrentExchangeService currentExchangeService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentExchangeService = currentExchangeService;
        }

        public async Task<OperationResult<Transaction>> TransferAsync(TransferDto transferDto)
        {
            try
            {
                var transferrate = await _currentExchangeService.GetMalaysiaSellRateAsync();
                Transaction transaction = _mapper.Map<Transaction>(transferDto);
                transaction.TransferRate = transferrate;
                transaction.PayoutAmount = transaction.TransferAmount * transferrate;
                transaction.TrasactionDateTime = DateTime.UtcNow;
                await _unitOfWork.Transactions.InsertAsync(transaction);
                int rowsaffected = await _unitOfWork.CompleteAsync();
                if (rowsaffected == 1)
                {
                    return new OperationResult<Transaction>(true, "Money Transfered Successfully.", rowsaffected, transaction);
                }
                return new OperationResult<Transaction>(false, "Money Failed To Transfer.", rowsaffected, null);
            }
            catch (Exception ex)
            {
                return new OperationResult<Transaction>(false, $"Exception Occurred : {ex.Message}", 0, null);
            }
        }
    }
}

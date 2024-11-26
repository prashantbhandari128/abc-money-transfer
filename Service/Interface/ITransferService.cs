using ABCMoneyTransfer.DTO;
using ABCMoneyTransfer.Persistence.Entities;
using ABCMoneyTransfer.Service.Result;

namespace ABCMoneyTransfer.Service.Interface
{
    public interface ITransferService
    {
        Task<OperationResult<Transaction>> TransferAsync(TransferDto transferDto);
    }
}

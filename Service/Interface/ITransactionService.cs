using ABCMoneyTransfer.Persistence.Entities;
using ABCMoneyTransfer.ViewModels;

namespace ABCMoneyTransfer.Service.Interface
{
    public interface ITransactionService
    {
        Task<Transaction?> GetTransactionAsync(Guid transactionid);
        Task<List<TransactionViewModel>> GetTransactionsAsync();
        Task<List<TransactionViewModel>> GetTransactionsAsync(DateTime? from, DateTime? to);
    }
}

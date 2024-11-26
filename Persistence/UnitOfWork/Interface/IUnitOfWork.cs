using ABCMoneyTransfer.Persistence.Repository.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace ABCMoneyTransfer.Persistence.UnitOfWork.Interface
{
    public interface IUnitOfWork
    {
        // Repositories
        ITransactionRepository Transactions { get; }

        // Save changes
        int Complete();
        Task<int> CompleteAsync();

        // Transaction
        IDbContextTransaction BeginTransaction();
    }
}

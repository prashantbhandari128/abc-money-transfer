using ABCMoneyTransfer.Persistence.Context;
using ABCMoneyTransfer.Persistence.Entities;
using ABCMoneyTransfer.Persistence.Repository.Interface;

namespace ABCMoneyTransfer.Persistence.Repository.Implementation
{
    public class TransactionRepository : Repository<Transaction>, ITransactionRepository
    {
        public TransactionRepository(AppDbContext context) : base(context)
        {

        }
    }
}

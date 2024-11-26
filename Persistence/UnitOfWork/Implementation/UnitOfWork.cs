using ABCMoneyTransfer.Persistence.Context;
using ABCMoneyTransfer.Persistence.Repository.Implementation;
using ABCMoneyTransfer.Persistence.Repository.Interface;
using ABCMoneyTransfer.Persistence.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore.Storage;

namespace ABCMoneyTransfer.Persistence.UnitOfWork.Implementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private bool _disposed = false;

        //-----------[ Add repositories here ]----------
        public ITransactionRepository Transactions { get; }
        //----------------------------------------------

        public UnitOfWork(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            //----------[ Initialize repositories here ]----------
            Transactions = new TransactionRepository(_context);
            //----------------------------------------------------
        }

        public int Complete() => _context.SaveChanges();

        public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();

        public IDbContextTransaction BeginTransaction() => _context.Database.BeginTransaction();

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

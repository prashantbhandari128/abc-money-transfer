using ABCMoneyTransfer.Persistence.Entities;
using ABCMoneyTransfer.Persistence.UnitOfWork.Interface;
using ABCMoneyTransfer.Service.Interface;
using ABCMoneyTransfer.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ABCMoneyTransfer.Service.Implementation
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransactionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Transaction?> GetTransactionAsync(Guid transactionid)
        {
            return await _unitOfWork.Transactions.FindAsync(transactionid);
        }

        public async Task<List<TransactionViewModel>> GetTransactionsAsync()
        {
            return await _unitOfWork.Transactions.GetQueryable().Select(transaction => new TransactionViewModel
            {
                TransactionId = transaction.TrasactionId,
                TransactionDate = transaction.TrasactionDateTime,
                SenderFullName = String.Join(" ", transaction.SenderFirstName, transaction.SenderMiddleName, transaction.SenderLastName),
                ReceiverFullName = String.Join(" ", transaction.RecieverFirstName, transaction.RecieverMiddleName, transaction.RecieverLastName),
                BankName = transaction.BankName,
                AccountNumber = transaction.AccountNumber,
                TransferAmount = transaction.TransferAmount,
                TransferRate = transaction.TransferRate,
                PayoutAmount = transaction.PayoutAmount
            }).ToListAsync();
        }

        public async Task<List<TransactionViewModel>> GetTransactionsAsync(DateTime? from, DateTime? to)
        {
            return await _unitOfWork.Transactions.GetQueryable().Where(x => x.TrasactionDateTime >= from && x.TrasactionDateTime <= to).Select(transaction => new TransactionViewModel
            {
                TransactionId = transaction.TrasactionId,
                TransactionDate = transaction.TrasactionDateTime,
                SenderFullName = String.Join(" ", transaction.SenderFirstName, transaction.SenderMiddleName, transaction.SenderLastName),
                ReceiverFullName = String.Join(" ", transaction.RecieverFirstName, transaction.RecieverMiddleName, transaction.RecieverLastName),
                BankName = transaction.BankName,
                AccountNumber = transaction.AccountNumber,
                TransferAmount = transaction.TransferAmount,
                TransferRate = transaction.TransferRate,
                PayoutAmount = transaction.PayoutAmount
            }).ToListAsync();
        }
    }
}

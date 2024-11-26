using System.ComponentModel.DataAnnotations;

namespace ABCMoneyTransfer.ViewModels
{
    public class TransactionViewModel
    {
        public Guid TransactionId { get; set; }
        public DateTime TransactionDate { get; set; }
        public String SenderFullName { get; set; } = String.Empty;
        public String ReceiverFullName { get; set; } = String.Empty;
        public String BankName { get; set; } = String.Empty;
        public String AccountNumber { get; set; } = String.Empty;
        public decimal TransferAmount { get; set; }
        public decimal TransferRate { get; set; }
        public decimal PayoutAmount { get; set; }
    }
}

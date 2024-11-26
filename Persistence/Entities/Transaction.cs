using System.ComponentModel.DataAnnotations;

namespace ABCMoneyTransfer.Persistence.Entities
{
    public class Transaction
    {
        [Key]
        public Guid TrasactionId { get; set; }

        [Required]  
        public String SenderFirstName { get; set; } = String.Empty;

        public String? SenderMiddleName { get; set; } = String.Empty;

        [Required]
        public String SenderLastName { get; set;} = String.Empty;

        [Required]
        public String SenderAddress { get; set;} = String.Empty;

        public String? SenderCountry { get; set;} = String.Empty;

        [Required]
        public String RecieverFirstName { get; set; } = String.Empty;

        public String? RecieverMiddleName { get; set; } = String.Empty;

        [Required]
        public String RecieverLastName { get; set; } = String.Empty;

        [Required]
        public String RecieverAddress { get; set; } = String.Empty;

        public String? RecieverCountry { get; set; } = String.Empty;

        [Required]
        public String BankName { get; set; } = String.Empty;

        [Required]
        public String AccountNumber { get; set; } = String.Empty;

        [Required]
        public decimal TransferAmount { get; set; }

        [Required]
        public decimal TransferRate { get; set; }

        [Required]
        public decimal PayoutAmount { get; set; }

        [Required]
        public DateTime TrasactionDateTime { get; set; }
    }
}

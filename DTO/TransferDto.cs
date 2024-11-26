using System.ComponentModel.DataAnnotations;

namespace ABCMoneyTransfer.DTO
{
    public class TransferDto
    {
        [Key]
        public Guid TrasactionId { get; set; }
        
        [Required(ErrorMessage = "Sender first name required.")]
        public String SenderFirstName { get; set; } = String.Empty;
        
        public String? SenderMiddleName { get; set; }
        
        [Required(ErrorMessage = "Sender last name required.")]
        public String SenderLastName { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Sender address required.")]
        public String SenderAddress { get; set; } = String.Empty;
        

        public String? SenderCountry { get; set; } 
        
        [Required(ErrorMessage = "Reciever first name required.")]
        public String RecieverFirstName { get; set; } = String.Empty;
        
        public String? RecieverMiddleName { get; set; }
        
        [Required(ErrorMessage = "Reciever last name required.")]
        public String RecieverLastName { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Reciever address required.")]
        public String RecieverAddress { get; set; } = String.Empty;

        public String? RecieverCountry { get; set; }
        
        [Required(ErrorMessage = "Bank Name is required.")]
        public String BankName { get; set; } = String.Empty;
        
        [Required(ErrorMessage = "Account number is required.")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Account number must be exactly 10 digits.")]
        public String AccountNumber { get; set; } = String.Empty;

        [Required(ErrorMessage = "Transfer amount is required.")]
        [Range(0.01, 1000000, ErrorMessage = "Transfer amount must be between 0.01 and 1,000,000.")]
        public decimal TransferAmount { get; set; }
    }
}

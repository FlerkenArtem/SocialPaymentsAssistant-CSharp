namespace SocialPaymentsAssistant.Models
{
    public class EmpApplicationData
    {
        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; } = string.Empty;
        public string PaymentType { get; set; } = string.Empty;
        public DateTime CreationDate { get; set; }
        public double Amount { get; set; }
        public string Status { get; set; } = string.Empty;
        public string AdditionalInfo { get; set; } = string.Empty;
    }
}
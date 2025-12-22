namespace SocialPaymentsAssistant.Models
{
    public class ApplicationData
    {
        public int ApplicationId { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public DateTime ApplicationDate { get; set; }
        public double Amount { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime? ConsiderationDate { get; set; }
    }
}
using System;

namespace SocialPaymentsAssistant.Models
{
    public class AcceptedApplicationData
    {
        public int ApplicationId { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public DateTime AcceptanceDate { get; set; }
        public double Amount { get; set; }
        public byte[] CertificateData { get; set; } = Array.Empty<byte>();
        public string CertificateFileName { get; set; } = string.Empty;
    }
}
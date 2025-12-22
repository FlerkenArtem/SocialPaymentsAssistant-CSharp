using System;
using System.Collections.Generic;
using System.Text;

namespace SocialPaymentsAssistant
{
    public class ApplicationInfo
    {
        public int ApplicationId { get; set; }
        public string ApplicantName { get; set; }
        public string PaymentType { get; set; }
        public DateTime CreationDate { get; set; }
        public double RequestedAmount { get; set; }
        public double ApprovedAmount { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePosition { get; set; }
        public string BranchName { get; set; }
        public string ApplicantAddress { get; set; }
        public string ApplicantInn { get; set; }
        public string ApplicantPhone { get; set; }
    }
}

using System;

namespace BothoClinic.Models
{
    public class Consultation
    {
        public int ConsultID { get; set; }
        public int ApptID { get; set; }
        public int ProviderID { get; set; }
        public string? Vitals { get; set; }
        public string? Notes { get; set; }
        public string? Diagnosis { get; set; }
        public string? Prescription { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
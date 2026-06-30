using System;

namespace BothoClinic.Models
{
    public class Appointment
    {
        public int ApptID { get; set; }
        public int PatientID { get; set; }
        public int ProviderID { get; set; }
        public DateTime ApptDateTime { get; set; }
        public string? Status { get; set; }
        public string? Reason { get; set; }
    }
}
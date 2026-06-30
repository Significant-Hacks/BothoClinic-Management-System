using System;

namespace BothoClinic.Models
{
    public class Patient
    {
        public int PatientID { get; set; }
        public int UserID { get; set; }
        public string? StudentID { get; set; }
        public DateTime DOB { get; set; }
        public string? Gender { get; set; }
        public string? BloodType { get; set; }
    }
}
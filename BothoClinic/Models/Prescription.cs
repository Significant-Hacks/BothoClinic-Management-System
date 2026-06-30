using System;

namespace BothoClinic.Models
{
    public class Prescription
    {
        public int PrescID { get; set; }
        public int ConsultID { get; set; }
        public int MedID { get; set; }
        public string? Dosage { get; set; }
        public string? Frequency { get; set; }
        public string? Duration { get; set; }
        public string? Instructions { get; set; }
    }
}
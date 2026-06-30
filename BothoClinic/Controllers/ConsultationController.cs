using BothoClinic.Models;
using System;
using System.Collections.Generic;
using System.Data;

namespace BothoClinic.Controllers
{
    public class ConsultationController
    {
        /// <summary>
        /// Create a new consultation record
        /// </summary>
        public static bool CreateConsultation(Consultation consultation)
        {
            try
            {
                string query = @"
                    INSERT INTO Consultations (
                        ApptID, ProviderID, Vitals, Notes, 
                        Diagnosis, Prescription, CreatedDate
                    ) VALUES (
                        @ApptID, @ProviderID, @Vitals, @Notes, 
                        @Diagnosis, @Prescription, GETDATE()
                    )";

                DatabaseHelper dbHelper = new DatabaseHelper();
                int rowsAffected = dbHelper.ExecuteNonQuery(query, new
                {
                    ApptID = consultation.ApptID,
                    ProviderID = consultation.ProviderID,
                    Vitals = consultation.Vitals,
                    Notes = consultation.Notes,
                    Diagnosis = consultation.Diagnosis,
                    Prescription = consultation.Prescription
                });
                
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating consultation: {ex.Message}");
            }
        }

        /// <summary>
        /// Get consultation by appointment ID
        /// </summary>
        public static Consultation GetConsultationByAppointmentID(int appointmentID)
        {
            try
            {
                string query = @"
                    SELECT 
                        ConsultID, ApptID, ProviderID, Vitals, 
                        Notes, Diagnosis, Prescription, CreatedDate
                    FROM Consultations 
                    WHERE ApptID = @ApptID";

                DatabaseHelper dbHelper = new DatabaseHelper();
                DataTable dataTable = dbHelper.ExecuteQuery(query, new { ApptID = appointmentID });
                
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return new Consultation
                    {
                        ConsultID = Convert.ToInt32(row["ConsultID"]),
                        ApptID = Convert.ToInt32(row["ApptID"]),
                        ProviderID = Convert.ToInt32(row["ProviderID"]),
                        Vitals = row["Vitals"] == DBNull.Value ? null : row["Vitals"].ToString(),
                        Notes = row["Notes"] == DBNull.Value ? null : row["Notes"].ToString(),
                        Diagnosis = row["Diagnosis"] == DBNull.Value ? null : row["Diagnosis"].ToString(),
                        Prescription = row["Prescription"] == DBNull.Value ? null : row["Prescription"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving consultation: {ex.Message}");
            }
            
            return null;
        }

        /// <summary>
        /// Update an existing consultation
        /// </summary>
        public static bool UpdateConsultation(Consultation consultation)
        {
            try
            {
                string query = @"
                    UPDATE Consultations 
                    SET Vitals = @Vitals,
                        Notes = @Notes,
                        Diagnosis = @Diagnosis,
                        Prescription = @Prescription
                    WHERE ConsultID = @ConsultID";

                DatabaseHelper dbHelper = new DatabaseHelper();
                int rowsAffected = dbHelper.ExecuteNonQuery(query, new
                {
                    ConsultID = consultation.ConsultID,
                    Vitals = consultation.Vitals,
                    Notes = consultation.Notes,
                    Diagnosis = consultation.Diagnosis,
                    Prescription = consultation.Prescription
                });
                
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating consultation: {ex.Message}");
            }
        }

        /// <summary>
        /// Get patient's consultation history
        /// </summary>
        public static List<Consultation> GetPatientConsultationHistory(int patientID)
        {
            List<Consultation> consultations = new List<Consultation>();
            
            try
            {
                string query = @"
                    SELECT 
                        c.ConsultID, c.ApptID, c.ProviderID, c.Vitals, 
                        c.Notes, c.Diagnosis, c.Prescription, c.CreatedDate
                    FROM Consultations c
                    INNER JOIN Appointments a ON c.ApptID = a.ApptID
                    WHERE a.PatientID = @PatientID
                    ORDER BY c.CreatedDate DESC";

                DatabaseHelper dbHelper = new DatabaseHelper();
                DataTable dataTable = dbHelper.ExecuteQuery(query, new { PatientID = patientID });
                
                foreach (DataRow row in dataTable.Rows)
                {
                    consultations.Add(new Consultation
                    {
                        ConsultID = Convert.ToInt32(row["ConsultID"]),
                        ApptID = Convert.ToInt32(row["ApptID"]),
                        ProviderID = Convert.ToInt32(row["ProviderID"]),
                        Vitals = row["Vitals"] == DBNull.Value ? null : row["Vitals"].ToString(),
                        Notes = row["Notes"] == DBNull.Value ? null : row["Notes"].ToString(),
                        Diagnosis = row["Diagnosis"] == DBNull.Value ? null : row["Diagnosis"].ToString(),
                        Prescription = row["Prescription"] == DBNull.Value ? null : row["Prescription"].ToString(),
                        CreatedDate = Convert.ToDateTime(row["CreatedDate"])
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving consultation history: {ex.Message}");
            }
            
            return consultations;
        }
    }
}
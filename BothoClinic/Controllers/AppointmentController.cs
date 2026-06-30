using BothoClinic.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace BothoClinic.Controllers
{
    public class AppointmentController
    {
        /// <summary>
        /// Get all appointments for a specific provider on a specific date
        /// </summary>
        public static List<Appointment> GetProviderAppointmentsByDate(int providerID, DateTime date)
        {
            List<Appointment> appointments = new List<Appointment>();
            
            try
            {
                string query = @"
                    SELECT 
                        a.AppointmentId AS ApptID,
                        a.PatientId,
                        a.ProviderId,
                        (CAST(a.AppointmentDate AS datetime) + CAST(a.TimeSlot AS datetime)) AS ApptDateTime,
                        a.Status,
                        a.Reason
                    FROM Appointments a
                    WHERE a.ProviderId = @ProviderID 
                    AND a.AppointmentDate = CAST(@Date AS DATE)
                    ORDER BY a.AppointmentDate, a.TimeSlot";

                DatabaseHelper dbHelper = new DatabaseHelper();
                DataTable dataTable = dbHelper.ExecuteQuery(query, new { ProviderID = providerID, Date = date });
                
                foreach (DataRow row in dataTable.Rows)
                {
                    appointments.Add(new Appointment
                    {
                        ApptID = Convert.ToInt32(row["ApptID"]),
                        PatientID = Convert.ToInt32(row["PatientID"]),
                        ProviderID = Convert.ToInt32(row["ProviderID"]),
                        ApptDateTime = Convert.ToDateTime(row["ApptDateTime"]),
                        Status = row["Status"] == DBNull.Value ? null : row["Status"].ToString(),
                        Reason = row["Reason"] == DBNull.Value ? null : row["Reason"].ToString()
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving appointments: {ex.Message}");
            }
            
            return appointments;
        }

        /// <summary>
        /// Get appointment details by ID
        /// </summary>
        public static Appointment GetAppointmentByID(int appointmentID)
        {
            try
            {
                string query = @"
                    SELECT 
                        a.AppointmentId AS ApptID,
                        a.PatientId,
                        a.ProviderId,
                        (CAST(a.AppointmentDate AS datetime) + CAST(a.TimeSlot AS datetime)) AS ApptDateTime,
                        a.Status,
                        a.Reason
                    FROM Appointments a
                    WHERE a.AppointmentId = @ApptID";

                DatabaseHelper dbHelper = new DatabaseHelper();
                DataTable dataTable = dbHelper.ExecuteQuery(query, new { ApptID = appointmentID });
                
                if (dataTable.Rows.Count > 0)
                {
                    DataRow row = dataTable.Rows[0];
                    return new Appointment
                    {
                        ApptID = Convert.ToInt32(row["ApptID"]),
                        PatientID = Convert.ToInt32(row["PatientID"]),
                        ProviderID = Convert.ToInt32(row["ProviderID"]),
                        ApptDateTime = Convert.ToDateTime(row["ApptDateTime"]),
                        Status = row["Status"] == DBNull.Value ? null : row["Status"].ToString(),
                        Reason = row["Reason"] == DBNull.Value ? null : row["Reason"].ToString()
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving appointment: {ex.Message}");
            }
            
            return null;
        }

        /// <summary>
        /// Update appointment status
        /// </summary>
        public static bool UpdateAppointmentStatus(int appointmentID, string status)
        {
            try
            {
                string query = @"
                    UPDATE Appointments 
                    SET Status = @Status
                    WHERE AppointmentId = @ApptID";

                DatabaseHelper dbHelper = new DatabaseHelper();
                int rowsAffected = dbHelper.ExecuteNonQuery(query, new { ApptID = appointmentID, Status = status });
                return rowsAffected > 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating appointment status: {ex.Message}");
            }
        }

        /// <summary>
        /// Get today's appointment count for a provider
        /// </summary>
        public static int GetTodayAppointmentCount(int providerID)
        {
            try
            {
                string query = @"
                    SELECT COUNT(*) 
                    FROM Appointments 
                    WHERE ProviderId = @ProviderID 
                    AND AppointmentDate = CAST(GETDATE() AS DATE)";

                DatabaseHelper dbHelper = new DatabaseHelper();
                object result = dbHelper.ExecuteScalar(query, new { ProviderID = providerID });
                return result != null ? Convert.ToInt32(result) : 0;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting appointment count: {ex.Message}");
            }
        }
    }
}
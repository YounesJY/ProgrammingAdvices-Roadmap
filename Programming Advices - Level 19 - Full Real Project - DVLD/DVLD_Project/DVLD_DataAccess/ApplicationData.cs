using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Common;

namespace DVLD_DataAccess
{
    public static class ApplicationData
    {
        /*
            this method retrieves all applications from the database and returns them as a DataTable.
            make sure you have a view named "ApplicationsList_View" in your database that contains the necessary columns for the applications.
        */
        public static DataTable GetAllApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM ApplicationsList_View 
                    ORDER BY ApplicationDate DESC
            ";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dataTable.Load(reader);

                reader.Close();
            }
            catch (Exception)
            {
                return dataTable;
            }
            finally
            {
                connection.Close();
            }

            return dataTable;
        }

        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID,
            ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM Applications 
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicantPersonID = (int)reader["ApplicantPersonID"];
                    ApplicationDate = (DateTime)reader["ApplicationDate"];
                    ApplicationTypeID = (int)reader["ApplicationTypeID"];
                    ApplicationStatus = (byte)reader["ApplicationStatus"];
                    LastStatusDate = (DateTime)reader["LastStatusDate"];
                    PaidFees = Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                }
                else
                    isFound = false;

                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int ApplicationID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    INSERT INTO Applications 
                    (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID)
                    VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID);
                    SELECT SCOPE_IDENTITY();
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    ApplicationID = insertedID;
            }
            catch (Exception)
            {
                ApplicationID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationID;
        }
        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID,
            byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE Applications
                    SET 
                        ApplicantPersonID = @ApplicantPersonID,
                        ApplicationDate = @ApplicationDate,
                        ApplicationTypeID = @ApplicationTypeID,
                        ApplicationStatus = @ApplicationStatus,
                        LastStatusDate = @LastStatusDate,
                        PaidFees = @PaidFees,
                        CreatedByUserID = @CreatedByUserID
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
        public static bool DeleteApplication(int ApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    DELETE FROM Applications
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }

        public static bool IsApplicationExist(int ApplicationID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT Found = 1 
                    FROM Applications 
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                isFound = reader.HasRows;
                reader.Close();
            }
            catch (Exception)
            {
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int ActiveApplicationID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT 
                        ActiveApplicationID = ApplicationID 
                    FROM Applications 
                    WHERE 
                        ApplicantPersonID = @ApplicantPersonID 
                        AND ApplicationTypeID = @ApplicationTypeID 
                        AND ApplicationStatus = 1
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int appID))
                    ActiveApplicationID = appID;
            }
            catch (Exception)
            {
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return (GetActiveApplicationID(PersonID, ApplicationTypeID) != ValidationConstants.INVALID_ID);
        }
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int ActiveApplicationID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT 
                        ActiveApplicationID = Applications.ApplicationID
                    FROM Applications 
                    INNER JOIN LocalDrivingLicenseApplications 
                        ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                    WHERE 
                        ApplicantPersonID = @ApplicantPersonID
                        AND ApplicationTypeID = @ApplicationTypeID
                        AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                        AND ApplicationStatus = 1
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int appID))
                    ActiveApplicationID = appID;
            }
            catch (Exception)
            {
                return ActiveApplicationID;
            }
            finally
            {
                connection.Close();
            }

            return ActiveApplicationID;
        }
        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE Applications
                    SET ApplicationStatus = @NewStatus,
                        LastStatusDate = @LastStatusDate
                    WHERE ApplicationID = @ApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@NewStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }
    }
}

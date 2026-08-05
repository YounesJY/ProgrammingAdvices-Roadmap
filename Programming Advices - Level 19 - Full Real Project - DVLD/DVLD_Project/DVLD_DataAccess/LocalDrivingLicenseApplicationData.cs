using DVLD_Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class LocalDrivingLicenseApplicationData
    {
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT *
                    FROM LocalDrivingLicenseApplications_View
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
        public static bool GetLocalDrivingLicenseApplicationByID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT *
                    FROM LocalDrivingLicenseApplications
                    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationID = (int)reader["ApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                else
                {
                    isFound = false;
                }

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
        public static bool GetLocalDrivingLicenseApplicationByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT *
                    FROM LocalDrivingLicenseApplications
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
                    LocalDrivingLicenseApplicationID = (int)reader["LocalDrivingLicenseApplicationID"];
                    LicenseClassID = (int)reader["LicenseClassID"];
                }
                else
                {
                    isFound = false;
                }

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

        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    INSERT INTO LocalDrivingLicenseApplications
                    (ApplicationID, LicenseClassID)
                    VALUES (@ApplicationID, @LicenseClassID);
                    SELECT SCOPE_IDENTITY();
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    LocalDrivingLicenseApplicationID = insertedID;
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return LocalDrivingLicenseApplicationID;
        }
        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE LocalDrivingLicenseApplications
                    SET ApplicationID = @ApplicationID,
                        LicenseClassID = @LicenseClassID
                    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);

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
        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    DELETE FROM LocalDrivingLicenseApplications
                    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

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

        // These methods are commented out because they are not currently used in the application,
        // but they will be used in the upcoming versions.
        /*
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT TOP 1 TestResult
                    FROM LocalDrivingLicenseApplications
                    INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                    INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                    WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                    AND TestAppointments.TestTypeID = @TestTypeID
                    ORDER BY TestAppointments.TestAppointmentID DESC
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object resultValue = command.ExecuteScalar();

                if (resultValue != null && bool.TryParse(resultValue.ToString(), out bool returnedResult))
                    result = returnedResult;
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT TOP 1 Found = 1
                    FROM LocalDrivingLicenseApplications
                    INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                    INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                    WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                    AND TestAppointments.TestTypeID = @TestTypeID
                    ORDER BY TestAppointments.TestAppointmentID DESC
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object resultValue = command.ExecuteScalar();

                if (resultValue != null)
                    isFound = true;
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte totalTrials = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT TotalTrialsPerTest = COUNT(TestID)
                    FROM LocalDrivingLicenseApplications
                    INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                    INNER JOIN Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                    WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                    AND TestAppointments.TestTypeID = @TestTypeID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object resultValue = command.ExecuteScalar();

                if (resultValue != null && byte.TryParse(resultValue.ToString(), out byte trials))
                    totalTrials = trials;
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return totalTrials;
        }
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT TOP 1 Found = 1
                    FROM LocalDrivingLicenseApplications
                    INNER JOIN TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID
                    WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID
                    AND TestAppointments.TestTypeID = @TestTypeID
                    AND IsLocked = 0
                    ORDER BY TestAppointments.TestAppointmentID DESC
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                object resultValue = command.ExecuteScalar();

                if (resultValue != null)
                    result = true;
            }
            catch (Exception)
            {
                //
            }
            finally
            {
                connection.Close();
            }

            return result;
        }
        */
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Common;

namespace DVLD_DataAccess
{
    public static class TestData
    {
        public static DataTable GetAllTests()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM Tests 
                    ORDER BY TestID
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
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM Tests 
                    WHERE TestID = @TestID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : string.Empty;
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
        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID,
            ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT TOP 1 
                        Tests.TestID, 
                        Tests.TestAppointmentID, 
                        Tests.TestResult, 
                        Tests.Notes, 
                        Tests.CreatedByUserID, 
                        Applications.ApplicantPersonID
                    FROM LocalDrivingLicenseApplications 
                    INNER JOIN Tests 
                        INNER JOIN TestAppointments 
                            ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID 
                        ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                    INNER JOIN Applications 
                        ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                    WHERE Applications.ApplicantPersonID = @PersonID 
                        AND LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID
                        AND TestAppointments.TestTypeID = @TestTypeID
                    ORDER BY Tests.TestAppointmentID DESC
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestID = (int)reader["TestID"];
                    TestAppointmentID = (int)reader["TestAppointmentID"];
                    TestResult = (bool)reader["TestResult"];
                    Notes = (reader["Notes"] != DBNull.Value) ? (string)reader["Notes"] : string.Empty;
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
        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int TestID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    INSERT INTO
                        Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID)
                    VALUES 
                        (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID);
                    
                    UPDATE TestAppointments 
                    SET IsLocked = 1 
                    WHERE TestAppointmentID = @TestAppointmentID;
                    
                    SELECT SCOPE_IDENTITY();
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty) ? Notes : (object)DBNull.Value);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    TestID = insertedID;
            }
            catch (Exception)
            {
                TestID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return TestID;
        }
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE Tests 
                    SET TestAppointmentID = @TestAppointmentID,
                        TestResult = @TestResult,
                        Notes = @Notes,
                        CreatedByUserID = @CreatedByUserID
                    WHERE TestID = @TestID
            ";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", (Notes != string.Empty) ? Notes : (object)DBNull.Value);
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
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT PassedTestCount = COUNT(TestTypeID)
                    FROM Tests INNER JOIN TestAppointments 
                        ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
                    WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestResult = 1
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && byte.TryParse(result.ToString(), out byte ptCount))
                    PassedTestCount = ptCount;
            }
            catch (Exception)
            {
                PassedTestCount = 0;
            }
            finally
            {
                connection.Close();
            }

            return PassedTestCount;
        }
    }
}
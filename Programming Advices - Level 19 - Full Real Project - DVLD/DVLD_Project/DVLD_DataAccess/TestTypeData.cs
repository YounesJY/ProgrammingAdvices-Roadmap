using DVLD_Common;
using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public class TestTypeData
    {
        public static DataTable GetAllTestTypes()
        {
            DataTable dt = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * FROM TestTypes 
                    ORDER BY TestTypeID
            ";

            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dt.Load(reader);

                reader.Close();
            }
            catch (Exception)
            {
                //Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return dt;
        }
        public static bool GetTestTypeInfoByID(int TestTypeID, ref string TestTypeTitle, ref string TestDescription, ref float TestFees)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    TestTypeTitle = (string)reader["TestTypeTitle"];
                    TestDescription = (string)reader["TestTypeDescription"];
                    TestFees = Convert.ToSingle(reader["TestTypeFees"]);
                }
                else
                {
                    isFound = false;
                }

                reader.Close();
            }
            catch (Exception)
            {
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        public static int AddNewTestType(string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int TestTypeID = ValidationConstants.INVALID_ID;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    INSERT INTO TestTypes (TestTypeTitle, TestTypeDescription, TestTypeFees)
                    VALUES (@TestTypeTitle, @TestTypeDescription, @TestTypeFees);
                    SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestTypeID = insertedID;
                }
            }
            catch (Exception)
            {
                //Console.WriteLine("Error: " + ex.Message);
                TestTypeID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return TestTypeID;
        }
        public static bool UpdateTestType(int TestTypeID, string TestTypeTitle, string TestTypeDescription, float TestTypeFees)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE TestTypes 
                    SET 
                        TestTypeTitle = @TestTypeTitle,
                        TestTypeDescription = @TestTypeDescription,
                        TestTypeFees = @TestTypeFees
                    WHERE TestTypeID = @TestTypeID;
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@TestTypeTitle", TestTypeTitle);
            command.Parameters.AddWithValue("@TestTypeDescription", TestTypeDescription);
            command.Parameters.AddWithValue("@TestTypeFees", TestTypeFees);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                // this can throw an exception if the archimetic value of Fees is too large for the database column type, or if the TestTypeID does not exist
                //Console.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                connection.Close();
            }

            return rowsAffected > 0;
        }
    }
}

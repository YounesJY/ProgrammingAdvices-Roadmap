using System;
using System.Data;
using System.Data.SqlClient;
using DVLD_Common;

namespace DVLD_DataAccess
{
    public static class ApplicationTypeData
    {
        public static DataTable GetAllApplicationTypes()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM ApplicationTypes 
                    ORDER BY ApplicationTypeTitle
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
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                SELECT * 
                FROM ApplicationTypes 
                WHERE ApplicationTypeID = @ApplicationTypeID
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    isFound = true;
                    ApplicationTypeTitle = (string)reader["ApplicationTypeTitle"];
                    ApplicationFees = Convert.ToSingle(reader["ApplicationFees"]);
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
        public static int AddNewApplicationType(string ApplicationTypeTitle, float ApplicationFees)
        {
            int ApplicationTypeID = ValidationConstants.INVALID_ID;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    INSERT INTO ApplicationTypes (ApplicationTypeTitle, ApplicationFees)
                    VALUES (@ApplicationTypeTitle, @ApplicationFees);
                    SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int id))
                    ApplicationTypeID = id;
            }
            catch (Exception)
            {
                ApplicationTypeID = ValidationConstants.INVALID_ID;
            }
            finally
            {
                connection.Close();
            }

            return ApplicationTypeID;
        }
        public static bool UpdateApplicationType(int ApplicationTypeID, string ApplicationTypeTitle, float ApplicationFees)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE ApplicationTypes
                    SET ApplicationTypeTitle = @ApplicationTypeTitle,
                        ApplicationFees = @ApplicationFees
                    WHERE ApplicationTypeID = @ApplicationTypeID
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", ApplicationTypeTitle);
            command.Parameters.AddWithValue("@ApplicationFees", ApplicationFees);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                isSuccess = (rowsAffected > 0);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            finally
            {
                connection.Close();
            }

            return isSuccess;
        }
        public static bool DeleteApplicationType(int ApplicationTypeID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    DELETE FROM ApplicationTypes 
                    WHERE ApplicationTypeID = @ApplicationTypeID
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);

            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();

                isSuccess = (rowsAffected > 0);
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            finally
            {
                connection.Close();
            }

            return isSuccess;
        }
    }
}

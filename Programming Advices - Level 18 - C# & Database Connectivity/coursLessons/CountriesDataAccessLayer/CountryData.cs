using System;
using System.Data;
using System.Data.SqlClient;

namespace CountriesDataAccessLayer
{
    public class CountryData
    {
        public static bool getCountryInfoByID(int CountryID, ref string CountryName, ref string Code, ref string PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    CountryID = (int)reader["CountryID"];
                    CountryName = (string)reader["CountryName"];
                    Code =  (reader["Code"] == DBNull.Value ? String.Empty: (string)reader["Code"]);
                    PhoneCode =  (reader["PhoneCode"] == DBNull.Value ? String.Empty: (string)reader["PhoneCode"]);
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

        public static bool getCountryInfoByName(ref int CountryID, ref string CountryName, ref string Code, ref string PhoneCode)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    CountryID = (int)reader["CountryID"];
                    CountryName = (string)reader["CountryName"];
                    Code = (reader["Code"] == DBNull.Value ? String.Empty : (string)reader["Code"]);
                    PhoneCode = (reader["PhoneCode"] == DBNull.Value ? String.Empty : (string)reader["PhoneCode"]);
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

        public static int addNewCountry(string CountryName, string Code, string PhoneCode)
        {
            //this function will return the new Country id if succeeded and -1 if not.
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                            INSERT INTO Countries (CountryName, Code, PhoneCode)
                            VALUES (@CountryName, @Code, @PhoneCode);
                             
                            SELECT SCOPE_IDENTITY();
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@Code", Code);
            command.Parameters.AddWithValue("@PhoneCode", PhoneCode);


            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    return insertedID;
                else
                    return -1;
            }

            catch (Exception)
            {
                return -1;
            }
            finally
            {
                connection.Close();
            }
        }

        public static bool updateCountry(int CountryID, string CountryName, string Code, string PhoneCode)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                            UPDATE  Countries  
                            SET 
                                CountryName = @CountryName,
                                Code = @Code,
                                PhoneCode = @PhoneCode
                            WHERE CountryID = @CountryID
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            command.Parameters.AddWithValue("@Code", Code);
            command.Parameters.AddWithValue("@PhoneCode", PhoneCode);

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

        public static DataTable getAllCountries()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Countries";
            SqlCommand command = new SqlCommand(query, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                    dataTable.Load(reader);

                reader.Close();
                connection.Close();

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

        public static bool deleteCountry(int CountryID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                            DELETE FROM Countries 
                            WHERE CountryID = @CountryID
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", CountryID);

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


        public static bool isCountryExist(int CountryID)
        {
            {
                bool isFound = false;
                SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
                string query = @"
                            SELECT TOP 1 1 
                            FROM Countries                    
                            WHERE CountryID = @CountryID
                            ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CountryID", CountryID);

                try
                {
                    connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    isFound = sqlDataReader.HasRows;
                    sqlDataReader.Close();
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }

                return isFound;
            }
        }
        public static bool isCountryExist(String CountryName)
        {
            {
                bool isFound = false;
                SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
                string query = @"
                            SELECT TOP 1 1 
                            FROM Countries                    
                            WHERE CountryName = @CountryName
                            ";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@CountryName", CountryName);

                try
                {
                    connection.Open();
                    SqlDataReader sqlDataReader = command.ExecuteReader();

                    isFound = sqlDataReader.HasRows;
                    sqlDataReader.Close();
                }
                catch (Exception)
                {
                    return false;
                }
                finally
                {
                    connection.Close();
                }

                return isFound;
            }
        }

    }
}

using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class PersonDataAccess
    {

        public static DataTable getPeople()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            DataTable dataTable = new DataTable();
            string query = "SELECT * FROM People";

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
        public static bool GetPersonInfoByID(int PersonID, ref string NationalNumber, ref string FirstName, ref string SecondName,
                                              ref string ThirdName, ref string LastName, ref byte Gender, ref DateTime DateOfBirth,
                                              ref string Address, ref string Phone, ref string Email, ref string ProfilePhotoPath,
                                              ref int CountryID, ref int CreatedByUser)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    SELECT * 
                    FROM People 
                    WHERE PersonID = @PersonID
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    NationalNumber = (string)reader["NationalNumber"];
                    FirstName = (string)reader["FirstName"];
                    SecondName = (reader["SecondName"] == DBNull.Value) ? "" : (string)reader["SecondName"];
                    ThirdName = (reader["ThirdName"] == DBNull.Value) ? "" : (string)reader["ThirdName"];
                    LastName = (string)reader["LastName"];
                    Gender = (byte)reader["Gender"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    Address = (string)reader["Address"];
                    Phone = (string)reader["Phone"];
                    Email = (reader["Email"] == DBNull.Value) ? "" : (string)reader["Email"];
                    ProfilePhotoPath = (reader["ImagePath"] == DBNull.Value) ? "" : (string)reader["ImagePath"];
                    CountryID = (int)reader["NationalityCountryID"];
                //  CreatedByUser = (int)reader["CreatedByUser"];
                }
                else
                {
                    // The record was not found
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
    }
}

using System;
using System.Data;
using System.Data.SqlClient;

namespace DVLD_DataAccess
{
    public static class PersonDataAccess
    {

        public static DataTable GetPeople()
        {
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            DataTable dataTable = new DataTable();
            string query = @"
                            SELECT 
                                People.PersonID,
                                People.NationalNumber,
                                People.FirstName,
                                People.SecondName,
                                People.ThirdName,
                                People.LastName,
                                CASE 
                                    WHEN People.Gender = 0 THEN 'Male'
                                    WHEN People.Gender = 1 THEN 'Female'
                                END AS Gender,
                                People.DateOfBirth,
                                People.Address,
                                People.Phone,
                                People.Email,
                                Countries.CountryName AS Nationality,
                                People.ProfilePhotoPath
                            FROM People
                            LEFT JOIN Countries ON People.NationalityCountryID = Countries.CountryID
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
                    ProfilePhotoPath = (reader["ProfilePhotoPath"] == DBNull.Value) ? "" : (string)reader["ProfilePhotoPath"];
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


        // ================ DAL Layer (PersonDataAccess.cs) ================

        public static int AddNewPerson(string NationalNumber, string FirstName, string SecondName, string ThirdName,
                                        string LastName, byte Gender, DateTime DateOfBirth, string Address,
                                        string Phone, string Email, string ProfilePhotoPath, int CountryID, int CreatedByUser)
        {
            int PersonID = -1;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    INSERT INTO People (NationalNumber, FirstName, SecondName, ThirdName, LastName, Gender, DateOfBirth, Address, Phone, Email, ProfilePhotoPath, NationalityCountryID)
                    VALUES 
                    (@NationalNumber, @FirstName, @SecondName, @ThirdName, @LastName, @Gender, @DateOfBirth, @Address, @Phone, @Email, @ProfilePhotoPath, @NationalityCountryID);
                    SELECT SCOPE_IDENTITY();
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNumber", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ThirdName", ThirdName);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (string.IsNullOrEmpty(Email))
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Email", Email);

            if (string.IsNullOrEmpty(ProfilePhotoPath))
                command.Parameters.AddWithValue("@ProfilePhotoPath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ProfilePhotoPath", ProfilePhotoPath);

            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            //command.Parameters.AddWithValue("@CreatedByUser", CreatedByUser);

            try
            {
                connection.Open();
                object result = command.ExecuteScalar();
                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                    PersonID = insertedID;
            }
            catch (Exception)
            {
                PersonID = -1;
            }
            finally
            {
                connection.Close();
            }

            return PersonID;
        }

        public static bool UpdatePerson(int PersonID, string NationalNumber, string FirstName, string SecondName,
                                         string ThirdName, string LastName, byte Gender, DateTime DateOfBirth,
                                         string Address, string Phone, string Email, string ProfilePhotoPath,
                                         int CountryID, int CreatedByUser)
        {
            int rowsAffected = 0;

            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                    UPDATE People 
                    SET NationalNumber = @NationalNumber,
                         FirstName = @FirstName,
                         SecondName = @SecondName,
                         ThirdName = @ThirdName,
                         LastName = @LastName,
                         Gender = @Gender,
                         DateOfBirth = @DateOfBirth,
                         Address = @Address,
                         Phone = @Phone,
                         Email = @Email,
                         ProfilePhotoPath = @ProfilePhotoPath,
                         NationalityCountryID = @NationalityCountryID
                    WHERE PersonID = @PersonID
            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@NationalNumber", NationalNumber);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);

            if (string.IsNullOrEmpty(ThirdName))
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ThirdName", ThirdName);

            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);

            if (string.IsNullOrEmpty(Email))
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            else
                command.Parameters.AddWithValue("@Email", Email);

            if (string.IsNullOrEmpty(ProfilePhotoPath))
                command.Parameters.AddWithValue("@ProfilePhotoPath", DBNull.Value);
            else
                command.Parameters.AddWithValue("@ProfilePhotoPath", ProfilePhotoPath);

            command.Parameters.AddWithValue("@NationalityCountryID", CountryID);
            //    command.Parameters.AddWithValue("@CreatedByUser", CreatedByUser);

            try
            {
                connection.Open();
                rowsAffected = command.ExecuteNonQuery();
            }
            catch (Exception)
            {
                rowsAffected = 0;
            }
            finally
            {
                connection.Close();
            }

            return (rowsAffected > 0);
        }


        public static bool IsPersonExist(int PersonID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM People WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);

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
        public static bool IsPersonExist(string NationalNumber)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT Found=1 FROM People WHERE NationalNumber = @NationalNumber";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNumber", NationalNumber);

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

        public static bool DeletePerson(int PersonID)
        {
            int rowsAffected = 0;

            using (SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString))
            using (SqlCommand command = new SqlCommand("DELETE FROM People WHERE PersonID = @PersonID", connection))
            {
                command.Parameters.AddWithValue("@PersonID", PersonID);
                try
                {
                    connection.Open();
                    rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    rowsAffected = 0;
                }
            }

            return (rowsAffected > 0);
        }
    }
}
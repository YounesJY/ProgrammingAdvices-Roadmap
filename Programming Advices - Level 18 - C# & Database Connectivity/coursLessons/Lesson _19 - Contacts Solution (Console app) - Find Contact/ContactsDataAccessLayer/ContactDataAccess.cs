using System;
using System.Data;
using System.Data.SqlClient;

namespace ContactsDataAccessLayer
{
    public class clsContactDataAccess
    {
        public static bool getContactInfoByID(int ID, ref string FirstName, ref string LastName, ref string Email, ref string Phone, ref string Address, ref DateTime DateOfBirth, ref int CountryID, ref string ImagePath)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    // The record was found
                    isFound = true;

                    FirstName = (string)reader["FirstName"];
                    LastName = (string)reader["LastName"];
                    Email = (string)reader["Email"];
                    Phone = (string)reader["Phone"];
                    Address = (string)reader["Address"];
                    DateOfBirth = (DateTime)reader["DateOfBirth"];
                    CountryID = (int)reader["CountryID"];

                    // ImagePath can bu NULL in the database --> it should be handled here
                    if (reader["ImagePath"] == DBNull.Value)
                        ImagePath = String.Empty;
                    else
                        ImagePath = (string)reader["ImagePath"];
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
                //Console.WriteLine("Error: " + ex.Message);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        
        public static int addNewContact(string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            //this function will return the new contact id if succeeded and -1 if not.
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                            INSERT INTO Contacts (FirstName, LastName, Email, Phone, Address,DateOfBirth, CountryID,ImagePath)
                            VALUES (@FirstName, @LastName, @Email, @Phone, @Address,@DateOfBirth, @CountryID,@ImagePath);
                             
                            SELECT SCOPE_IDENTITY();
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);

            // Handle saving NULL in database
            if (ImagePath == String.Empty)
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("@ImagePath", ImagePath);

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
        
        public static bool updateContact(int ID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                            UPDATE  Contacts  
                            SET 
                                FirstName = @FirstName, 
                                LastName = @LastName, 
                                Email = @Email, 
                                Phone = @Phone, 
                                Address = @Address, 
                                DateOfBirth = @DateOfBirth,
                                CountryID = @CountryID,
                                ImagePath =@ImagePath
                            WHERE ContactID = @ContactID
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@Email", Email);
            command.Parameters.AddWithValue("@Phone", Phone);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@CountryID", CountryID);
            // Handle saving NULL in database
            if (ImagePath == String.Empty)
                command.Parameters.AddWithValue("@ImagePath", System.DBNull.Value);
            else
                command.Parameters.AddWithValue("@ImagePath", ImagePath);

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
        
        public static DataTable getAllContacts()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);

            string query = "SELECT * FROM Contacts";
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
        
        public static bool deleteContact(int ContactID)
        {
            int rowsAffected = 0;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                            DELETE FROM Contacts 
                            WHERE ContactID = @ContactID
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);

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
        
        public static bool isContactExist(int ContactID)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(DataAccessSettings.ConnectionString);
            string query = @"
                            SELECT TOP 1 1 
                            FROM Contacts                    
                            WHERE ContactID = @ContactID
                            ";

            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ContactID", ContactID);

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

using System;
using System.Data.SqlClient;

class Program
{
    static void Main()
    {
        string connectionString = "Server=your_server;Database=your_database;User Id=your_username;Password=your_password;";

        /*
        ==============
        == Remember ==  => SqlConnection, SqlCommand and SqlDataReader all implements the IDisposable Iface
        ==============
            
    
            // Summary:
            //     Provides a mechanism for releasing unmanaged resources.
            [ComVisible(true)]
            [__DynamicallyInvokable]
            public interface IDisposable
            {
                //
                // Summary:
                //     Performs application-defined tasks associated with freeing, releasing, or resetting
                //     unmanaged resources.
                [__DynamicallyInvokable]
                void Dispose();
            }

        */



        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT FirstName, LastName FROM Employee";

                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                string firstName = reader["FirstName"].ToString();
                                string lastName = reader["LastName"].ToString();
                                Console.WriteLine($"Name: {firstName} {lastName}");
                            }
                        }
                        else
                            Console.WriteLine("No rows found.");
                    }
                }
            }
        }
        catch (SqlException ex)
        {
            Console.WriteLine("Database connection error: " + ex.Message);
        }
    }
}

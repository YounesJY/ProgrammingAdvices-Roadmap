using System;
using System.Data;
using System.Net;
using System.Data.SqlClient;

public class Program
{
    static string connectionString = "Server=.;Database=ContactsDB;User Id=sa;Password=JY0912;"; // Replace with your actual connection string

    static void deleteContacts(string ContactIDs)
    {

        SqlConnection connection = new SqlConnection(connectionString);
        string query = @"
                        DELETE Contacts 
                        WHERE ContactID IN (" + ContactIDs + ")";
        SqlCommand command = new SqlCommand(query, connection);
             
                try
                {
                    connection.Open();

                    int rowsAffected = command.ExecuteNonQuery();

                    if (rowsAffected > 0)
                        Console.WriteLine("Record Deleted successfully.");
                    else
                        Console.WriteLine("No Records Deleted.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }

                connection.Close();
    }

    public static void Main()
    {

       
        deleteContacts("8,19,11");

        Console.ReadKey();

    }

}

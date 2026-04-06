using System;
using System.Data.SqlClient;


namespace Lesson__10___Retrieve_a_Single_Value
{
    internal class Program
    {
        private static string connectionString = "Server=.; DataBase=ContactsDB; User ID=sa; Password=JY0912";

        static string getFirstName(int contactID)
        {
            string firstname = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            // ========================================
            // ========= VERY IMPORTANT NOTE ==========
            // ========================================
            // If multiple queries are executed at once,
            // the sqlCommand.ExecuteScalar() will return "the result of the first query" [THE FIRST COLUMN of THE FIRST ROW of THE FIRST RESULTSET] 
            string query = @"
                            SELECT 
                                email, lastname, firstname
                            FROM Contacts;
                
                            SELECT lastname
                            FROM Contacts
                            WHERE ContactID = @ContactID;            
            ";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ContactID", contactID);


            try
            {
                sqlConnection.Open();
                Object result = sqlCommand.ExecuteScalar();

                firstname = (result == null) ? null : result.ToString();

            }
            catch (Exception ex)
            {
                Console.WriteLine($" - Exception : {ex.Message}");
            }

            sqlConnection.Close();

            return firstname;
        }

        static void Main(string[] args)
        {
            Console.WriteLine(getFirstName(2));
            Console.ReadKey();
        }
    }
}

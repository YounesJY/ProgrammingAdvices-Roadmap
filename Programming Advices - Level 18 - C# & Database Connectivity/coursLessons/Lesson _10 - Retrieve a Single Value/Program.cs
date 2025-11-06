using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson__10___Retrieve_a_Single_Value
{
    internal class Program
    {

        private static string connectionString = "Server=.; DataBase=ContactsDB; User ID=sa; Password=JY0912";

        static string getFirstName(int contactID)
        {
            String firstname = null;
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            string query = " SELECT email FROM Contacts WHERE ContactID = @ContactID;" +
                "SELECT lastname FROM Contacts WHERE ContactID = @ContactID;";

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
            Console.WriteLine(getFirstName(4));

            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson__15___Delete_Data
{
    internal class Program
    {
        class Contact
        {
            public int contactID { get; set; }
            public string firstname { get; set; }
            public string lastname { get; set; }
            public string email { get; set; }
            public string phone { get; set; }
            public string address { get; set; }
            public int countryID { get; set; }


            //public Contact(int contactID, string firstname, string lastname, string email, string phone, string address, int countryID)
            //{
            //    this.contactID = contactID;
            //    this.firstname = firstname;
            //    this.lastname = lastname;
            //    this.email = email;
            //    this.phone = phone;
            //    this.address = address;
            //    this.countryID = countryID;
            //}
        }

        private static string connectionString = "Server=.; DataBase=ContactsDB; User ID=sa; Password=JY0912";


        static void deleteContact(int contactID)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string query = @"DELETE FROM Contacts
                             WHERE ContactID >=12
                            ";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                short affectedRows = (short)sqlCommand.ExecuteNonQuery();

                if (affectedRows > 0)
                    Console.WriteLine(" Rows deleted successefully !");
                else
                    Console.WriteLine(" Rows deletion failed !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" - Exception : {ex.Message}");
            }

            sqlConnection.Close();
        }

        static void Main(string[] args)
        {

            deleteContact(15);
            Console.ReadKey();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson__13___Retrieve_Auto_Number_Inserting_Data
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


        static void addNewContact(Contact contact)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string query = @"
                             INSERT INTO Contacts
                             VALUES 
                             (@FirstName, @LastName, @Email, @Phone, @Address, @CountryID);
                             SELECT SCOPE_IDENTITY()
            ";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@FirstName", contact.firstname);
            sqlCommand.Parameters.AddWithValue("@LastName", contact.lastname);
            sqlCommand.Parameters.AddWithValue("@Email", contact.email);
            sqlCommand.Parameters.AddWithValue("@Phone", contact.phone);
            sqlCommand.Parameters.AddWithValue("@Address", contact.address);
            sqlCommand.Parameters.AddWithValue("@CountryID", contact.countryID);

            try
            {
                int newlyInsertedIdentity;

                sqlConnection.Open();
                object result = sqlCommand.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out newlyInsertedIdentity))
                {
                    Console.WriteLine(" Rows inserted successefully !");
                    Console.WriteLine($" Newly inserted Identity : {newlyInsertedIdentity}");
                }
                else
                    Console.WriteLine(" Rows insertertion failed !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" - Exception : {ex.Message}");
            }

            sqlConnection.Close();
        }

        static void Main(string[] args)
        {

            addNewContact(new Contact
            {
                firstname = "red",
                lastname = "hat",
                email = "redhat@linux.ker",
                phone = "128831",
                address = "lin 12 sa",
                countryID = 2
            });
            Console.ReadKey();
        }
    }
}

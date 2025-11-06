using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson__14___Update_Data
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


        static void updateContact(Contact contact)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string query = @"
                            UPDATE Contacts
                            SET
                                            firstname = @FirstName,
                                            lastname = @LastName,
                                            email = @Email,
                                            phone = @Phone,
                                            address = @Address,
                                            countryID = @CountryID
                            WHERE ContactID >= 16 
            ";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@ContactID", contact.contactID);
            sqlCommand.Parameters.AddWithValue("@FirstName", contact.firstname);
            sqlCommand.Parameters.AddWithValue("@LastName", contact.lastname);
            sqlCommand.Parameters.AddWithValue("@Email", contact.email);
            sqlCommand.Parameters.AddWithValue("@Phone", contact.phone);
            sqlCommand.Parameters.AddWithValue("@Address", contact.address);
            sqlCommand.Parameters.AddWithValue("@CountryID", contact.countryID);

            try
            {
                sqlConnection.Open();
                short affectedRows = (short)sqlCommand.ExecuteNonQuery();

                if (affectedRows > 0)
                    Console.WriteLine(" Rows updated successefully !");
                else
                    Console.WriteLine(" Rows update failed !");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" - Exception : {ex.Message}");
            }

            sqlConnection.Close();
        }

        static void Main(string[] args)
        {

            updateContact(new Contact
            {
                contactID = 24,
                firstname = "taylor",
                lastname = "vlad",
                email = "cstatlor@gmail.com",
                phone = "897689",
                address = "elm stree 32 north",
                countryID = 5
            });
            Console.ReadKey();
        }
    }
}

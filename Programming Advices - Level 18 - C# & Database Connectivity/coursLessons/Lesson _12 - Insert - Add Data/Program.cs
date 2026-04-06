using System;
using System.Data.SqlClient;

namespace Lesson__12___Insert___Add_Data
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

            // Using an object initializer instead of a constructor
            // since we don't have the contactID field to be inserted [WE SHOULDN't INSERT IT MANUALY]
            // AND this contactID should be returned after inserting/creating the new record

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
                            INSERT INTO 
                                Contacts 
                            VALUES 
                                (@FirstName, @LastName, @Email, @Phone, @Address, @CountryID)
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
                sqlConnection.Open();
                short affectedRows = (short)sqlCommand.ExecuteNonQuery();

                if (affectedRows > 0)
                    Console.WriteLine(" Rows inserted successefully !");
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
            // if this exception occurs, this means you should have the exact same DB table shema + ID being AutoIncrement
            // for me. this means that i have changed the DB shema due to future lessons/requirements
            // -Exception : Column name or number of supplied values does not match table definition.                                                                                                                                                                                                                

            addNewContact(new Contact
            {
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

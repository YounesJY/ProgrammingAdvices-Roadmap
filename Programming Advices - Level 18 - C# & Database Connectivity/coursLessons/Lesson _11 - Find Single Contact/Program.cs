using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson__11___Find_Single_Contact
{
    internal class Program
    {
        class Contact
        {
            public int contactID { get; }
            public string firstname { get; }
            public string lastname { get; }
            public string email { get; }
            public string phone { get; }
            public string address { get; }
            public int countryID { get; }


            public Contact(int contactID, string firstname, string lastname, string email, string phone, string address, int countryID)
            {
                this.contactID = contactID;
                this.firstname = firstname;
                this.lastname = lastname;
                this.email = email;
                this.phone = phone;
                this.address = address;
                this.countryID = countryID;
            }
        }

        private static string connectionString = "Server=.; DataBase=ContactsDB; User ID=sa; Password=JY0912";

        static Contact ConvertRecordToObject(SqlDataReader sqlDataReader)
        {
            return new Contact(
                Convert.ToInt32(sqlDataReader["ContactID"]),
                Convert.ToString(sqlDataReader["Firstname"]),
                Convert.ToString(sqlDataReader["Lastname"]),
                Convert.ToString(sqlDataReader["Email"]),
                Convert.ToString(sqlDataReader["Phone"]),
                Convert.ToString(sqlDataReader["Address"]),
                Convert.ToInt32(sqlDataReader["CountryID"])
            );
        }
        static void printContact(Contact contact)
        {
            Console.WriteLine($" - ContactID : {contact.contactID}");
            Console.WriteLine($" - Firstname : {contact.firstname}");
            Console.WriteLine($" - Lastname  : {contact.lastname}");
            Console.WriteLine($" - Email     : {contact.email}");
            Console.WriteLine($" - Phone     : {contact.phone}");
            Console.WriteLine($" - Address   : {contact.address}");
            Console.WriteLine($" - CountryID : {contact.countryID}");
            Console.WriteLine();
        }
        static bool findContactByID(ref Contact contact, int contactID)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Contacts WHERE ContactID = @ContactID";

            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            sqlCommand.Parameters.AddWithValue("@ContactID", contactID);

            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                    contact = ConvertRecordToObject(sqlDataReader);

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" - Exception : {ex.Message}");
            }

            sqlConnection.Close();

            return (contact == null) ? false : true;
        }

        static void Main(string[] args)
        {
            Contact contact = null;


            if (findContactByID(ref contact, 6))
            {
                Console.WriteLine(" Contact found : ");
                printContact(contact);
            }
            else
                Console.WriteLine(" Contact not found !");

            Console.ReadKey();
        }
    }
}

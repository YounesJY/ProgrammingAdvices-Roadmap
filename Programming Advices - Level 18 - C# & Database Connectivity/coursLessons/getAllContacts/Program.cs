using System;
using System.Data.SqlClient;



namespace getAllContacts
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
            Console.WriteLine($" - Lastname : {contact.lastname}");
            Console.WriteLine($" - Email : {contact.email}");
            Console.WriteLine($" - Phone : {contact.phone}");
            Console.WriteLine($" - Address : {contact.address}");
            Console.WriteLine($" - CountryID : {contact.countryID}");
            Console.WriteLine();
        }
        static void printAllContacts()
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string query = "SELECT * FROM Contacts";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                    printContact(ConvertRecordToObject(sqlDataReader));

                sqlDataReader.Close();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" - Exception : {ex.Message}");
            }

            sqlConnection.Close();
        }

        static void Main(string[] args)
        {
            printAllContacts();
            Console.ReadKey();
        }
    }
}

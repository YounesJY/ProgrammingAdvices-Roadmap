using System;
using System.Data.SqlClient;


namespace Lesson__09___Parameterized_Query_With_Like
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


        static void findContactsPattern(string query, string firstname)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);

            sqlCommand.Parameters.AddWithValue("@FirstName", firstname);

            try
            {
                sqlConnection.Open();
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                    printContact(ConvertRecordToObject(sqlDataReader));

                sqlDataReader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($" - Exception : {ex.Message}");
            }

            sqlConnection.Close();
        }

        static void findContactsStartsWith(string firstname)
        {
            findContactsPattern("SELECT * FROM Contacts WHERE FirstName LIKE '' + @FirstName + '%'", firstname);
        }
        static void findContactsEndsWith(string firstname)
        {
            findContactsPattern("SELECT * FROM Contacts WHERE FirstName LIKE '%' + @FirstName + ''", firstname);
        }
        static void findContactsContains(string firstname)
        {
            findContactsPattern("SELECT * FROM Contacts WHERE FirstName LIKE '%' + @FirstName + '%'", firstname);
        }

        static void Main(string[] args)
        {
            Console.WriteLine("  All contacts that starts with 'j'");
            findContactsStartsWith("j");

            Console.WriteLine("  All contacts that ends with 'n' ");
            findContactsEndsWith("n");

            Console.WriteLine("  All contacts that contains 'h'");
            findContactsContains("h");


            Console.ReadKey();
        }
    }
}

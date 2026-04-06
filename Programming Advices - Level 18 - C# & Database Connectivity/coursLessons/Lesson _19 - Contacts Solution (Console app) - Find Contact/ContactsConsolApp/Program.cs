using ContactsBusinessLayer;
using System;
using System.Data;

namespace ContactsConsolApp_PresentationLayer
{
    internal class Program
    {
        static void testfindContact(int ID)
        {
            clsContact contact = clsContact.find(ID);
            if (contact != null)
            {
                Console.WriteLine(contact.FirstName + " " + contact.LastName);
                Console.WriteLine(contact.Email);
                Console.WriteLine(contact.Phone);
                Console.WriteLine(contact.Address);
                Console.WriteLine(contact.DateOfBirth);
                Console.WriteLine(contact.CountryID);
                Console.WriteLine(contact.ImagePath);
            }
            else
                Console.WriteLine($"Contact [{ID}] Not found!");
        }
        static void testAddNewContact()
        {
            clsContact contact = new clsContact();

            contact.FirstName = "Fadi";
            contact.LastName = "Maher";
            contact.Email = "A@a.com";
            contact.Phone = "010010";
            contact.Address = "address1";
            contact.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
            contact.CountryID = 1;
            contact.ImagePath = "";

            if (contact.save())
                Console.WriteLine($"Contact Added Successfully with id= {contact.ID}");
        }
        static void testUpdateContact(int ID)
        {
            clsContact contact = clsContact.find(ID);

            if (contact != null)
            {
                //update whatever info you want
                contact.FirstName = "Unis";
                contact.LastName = "JY";
                contact.Email = "unixjy@a.com";
                contact.Phone = "0912231231";
                contact.Address = "222";
                contact.DateOfBirth = new DateTime(1977, 11, 6, 10, 30, 0);
                contact.CountryID = 1;
                contact.ImagePath = "";

                if (contact.save())
                    Console.WriteLine("Contact updated Successfully !");
                else
                    Console.WriteLine("Contact update failed !");
            }
            else
                Console.WriteLine("Contact not found!");
        }
        static void listContacts()
        {
            DataTable dataTable = clsContact.getAllContacts();

            Console.WriteLine("Contacts Data: ");
            foreach (DataRow row in dataTable.Rows)
                Console.WriteLine($"[{row["ContactID"]}] |-|  {row["FirstName"]} {row["LastName"]}");
        }

        static void Main(string[] args)
        {
            testfindContact(2);
            // testAddNewContact();
            // testUpdateContact(1);
            // listContacts();

            Console.ReadKey();
        }
    }
}

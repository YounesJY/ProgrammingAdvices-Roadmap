using ContactsBusinessLayer;
using CountriesBusinessLayer;
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
                Console.WriteLine("Contact [" + ID + "] Not found!");
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
                Console.WriteLine("Contact Added Successfully with id=" + contact.ID);
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
        static void testDeleteContact(int ID)
        {
            if (clsContact.isContactExist(ID))
            {
                if (clsContact.deleteContact(ID))
                    Console.WriteLine("Contact deleted Successfully !");
                else
                    Console.WriteLine("Contact deletion failed !");
            }
            else
                Console.WriteLine("Contact not found !");
        }
        static void testIsContactExist(int ID)
        {
            if (clsContact.isContactExist(ID))
                Console.WriteLine("Contact found !");
            else
                Console.WriteLine("Contact not found !");
        }
        static void listContacts()
        {
            DataTable dataTable = clsContact.getAllContacts();

            Console.WriteLine("Contacts Data:");
            foreach (DataRow row in dataTable.Rows)
                Console.WriteLine($"[{row["ContactID"]}] |-|  {row["FirstName"]} {row["LastName"]}");
        }


        //---Test Country Business
        static void testfindCountryByID(int ID)
        {
            Country Country1 = Country.find(ID);

            if (Country1 != null)
            {
                Console.WriteLine("Name: " + Country1.CountryName);
                Console.WriteLine("Code: " + Country1.Code);
                Console.WriteLine("PhoneCode: " + Country1.PhoneCode);
            }
            else
                Console.WriteLine("Country [" + ID + "] Not found!");
        }
        static void testfindCountryByName(string CountryName)
        {
            Country Country1 = Country.find(CountryName);

            if (Country1 != null)
            {
                Console.WriteLine("Country [" + CountryName + "] isFound with ID = " + Country1.CountryID);
                Console.WriteLine("Name: " + Country1.CountryName);
                Console.WriteLine("Code: " + Country1.Code);
                Console.WriteLine("PhoneCode: " + Country1.PhoneCode);
            }
            else
                Console.WriteLine("Country [" + CountryName + "] Is Not found!");
        }
        static void testIsCountryExistByID(int ID)
        {
            if (Country.isCountryExist(ID))
                Console.WriteLine("Yes, Country is there.");
            else
                Console.WriteLine("No, Country Is not there.");
        }
        static void testIsCountryExistByName(string CountryName)
        {
            if (Country.isCountryExist(CountryName))
                Console.WriteLine("Yes, Country is there.");
            else
                Console.WriteLine("No, Country Is not there.");
        }
        static void testAddNewCountry()
        {
            Country Country1 = new Country();

            Country1.CountryName = "Eygpt";
            Country1.Code = "222";
            Country1.PhoneCode = "001";
            if (Country1.save())
                Console.WriteLine("Country Added Successfully with id=" + Country1.CountryID);
        }
        static void testUpdateCountry(int ID)
        {
            Country Country1 = Country.find(ID);

            if (Country1 != null)
            {
                //update whatever info you want
                Country1.CountryName = "Egypt";
                Country1.Code = "111";
                Country1.PhoneCode = "555";

                if (Country1.save())
                    Console.WriteLine("Country updated Successfully ");
            }
            else
                Console.WriteLine("Country is you want to update is Not found!");
        }
        static void testDeleteCountry(int ID)
        {
            if (Country.isCountryExist(ID))
                if (Country.deleteCountry(ID))
                    Console.WriteLine("Country Deleted Successfully.");
                else
                    Console.WriteLine("Faild to delete Country.");
            else
                Console.WriteLine("Faild to delete: The Country with id = " + ID + " is not found");
        }
        static void ListCountries()
        {
            DataTable dataTable = Country.getAllCountries();

            Console.WriteLine("Coutries Data:");
            foreach (DataRow row in dataTable.Rows)
                Console.WriteLine($"{row["CountryID"]},  {row["CountryName"]} , {row["Code"]}, {row["PhoneCode"]}");
        }

        static void Main(string[] args)
        {
            // testfindContact(1);
            // testAddNewContact();
            // testUpdateContact(1);
            // testDeleteContact(6);
            // testIsContactExist(6);
            // listContacts();

            testfindCountryByID(5);
            testfindCountryByID(50);

            testAddNewCountry();

            testUpdateCountry(6);

            testDeleteCountry(6);
            testDeleteCountry(7);
            testDeleteCountry(8);
            testDeleteCountry(9);
            testDeleteCountry(10);

            testIsCountryExistByID(100);
            testIsCountryExistByID(10);
            testIsCountryExistByID(1);

            ListCountries();

            testfindCountryByName("Canada");
            testfindCountryByName("united kingdom");

            Console.ReadKey();
        }
    }
}

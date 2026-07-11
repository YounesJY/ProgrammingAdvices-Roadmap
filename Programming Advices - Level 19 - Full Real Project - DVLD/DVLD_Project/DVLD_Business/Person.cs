using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DVLD_DataAccess;

namespace DVLD_Business
{
    public class Person
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public int PersonID { get; private set; }
        public string NationalNumber { get; private set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Name { get => $"{FirstName} {SecondName} {ThirdName} {LastName}"; }
        public byte Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoPath { get; set; }
        public int CountryID { get; set; }
        public int CreatedByUser { get; set; }
        private enMode _Mode = enMode.AddNew;


        private Person(int PersonID, string NationalNumber, string FirstName, string SecondName, string ThirdName,
                        string LastName, byte Gender, DateTime DateOfBirth, string Address, string Phone,
                        string Email, string ProfilePhotoPath, int CountryID, int CreatedByUser)
        {
            this.PersonID = PersonID;
            this.NationalNumber = NationalNumber;
            this.FirstName = FirstName;
            this.SecondName = SecondName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.Gender = Gender;
            this.DateOfBirth = DateOfBirth;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.ProfilePhotoPath = ProfilePhotoPath;
            this.CountryID = CountryID;
            this.CreatedByUser = CreatedByUser;
            this._Mode = enMode.Update;
        }

        public Person() : this(-1, "", "", "", "", "", 0, DateTime.Now, "", "", "", "", -1, -1)
        {
            this._Mode = enMode.AddNew;
        }

        public static DataTable GetPeople()
        {
            return PersonDataAccess.GetPeople();
        }

        public static Person Find(string NationalNumber)
        {
            int PersonID = -1;
            string FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            string Address = "", Phone = "", Email = "", ProfilePhotoPath = "";
            byte Gender = 0;
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;
            int CreatedByUser = -1;


            if (PersonDataAccess.GetPersonInfoByID(PersonID, ref NationalNumber, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref Gender, ref DateOfBirth, ref Address, ref Phone, ref Email, ref ProfilePhotoPath,
            ref CountryID, ref CreatedByUser))
                return new Person(PersonID, NationalNumber, FirstName, SecondName, ThirdName, LastName,
                Gender, DateOfBirth, Address, Phone, Email, ProfilePhotoPath, CountryID, CreatedByUser);

            return null;
        }
        public static Person Find(int PersonID)
        {
            string NationalNumber = "", FirstName = "", SecondName = "", ThirdName = "", LastName = "";
            string Address = "", Phone = "", Email = "", ProfilePhotoPath = "";
            byte Gender = 0;
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;
            int CreatedByUser = -1;


            if (PersonDataAccess.GetPersonInfoByID(PersonID, ref NationalNumber, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref Gender, ref DateOfBirth, ref Address, ref Phone, ref Email, ref ProfilePhotoPath,
            ref CountryID, ref CreatedByUser))
                return new Person(PersonID, NationalNumber, FirstName, SecondName, ThirdName, LastName,
                Gender, DateOfBirth, Address, Phone, Email, ProfilePhotoPath, CountryID, CreatedByUser);

            return null;
        }

        public static bool IsPersonExist(int PersonID)
        {
            return PersonDataAccess.IsPersonExist(PersonID);
        }
        public static bool IsPersonExist(string NationalNumber)
        {
            return PersonDataAccess.IsPersonExist(NationalNumber);
        }
    }
}

using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class Person
    {
        public enum enMode : byte { AddNew = 0, Update = 1 };
        public enum enGender : byte { Male, Female };


        private enMode _Mode = enMode.AddNew;
        private string _NationalNumber;

        public int PersonID { get; private set; }
        public string NationalNumber
        {
            get { return _NationalNumber; }
            set
            {
                /* You should handle this
                if (_Mode == enMode.Update)
                {
                    throw new InvalidOperationException("Cannot modify NationalNumber when in Update mode.");
                }
                */
                _NationalNumber = value;
            }
        }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string ThirdName { get; set; }
        public string LastName { get; set; }
        public string Name { get => $"{FirstName} {SecondName} {ThirdName} {LastName}"; }
        public enGender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ProfilePhotoPath { get; set; }
        public Country CountryInfo { get; set; }
        public int CreatedByUser { get; set; }


        private Person(int PersonID, string NationalNumber, string FirstName, string SecondName, string ThirdName,
                        string LastName, enGender Gender, DateTime DateOfBirth, string Address, string Phone,
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
            this.CountryInfo = Country.Find(CountryID);
            this.CreatedByUser = CreatedByUser;
            this._Mode = enMode.Update;
        }
        public Person() : this(ValidationConstants.INVALID_ID, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, enGender.Male, DateTime.Now, string.Empty, string.Empty, string.Empty, null, ValidationConstants.INVALID_ID, ValidationConstants.INVALID_ID)
        {
            this._Mode = enMode.AddNew;
        }

        public static DataTable GetPeople()
        {
            return PersonDataAccess.GetPeople();
        }
        public static Person Find(int PersonID)
        {
            string NationalNumber = string.Empty, FirstName = string.Empty, SecondName = string.Empty, ThirdName = string.Empty, LastName = string.Empty;
            string Address = string.Empty, Phone = string.Empty, Email = string.Empty, ProfilePhotoPath = null;
            byte Gender = (byte)enGender.Male;
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = ValidationConstants.INVALID_ID;
            int CreatedByUser = ValidationConstants.INVALID_ID;


            if (PersonDataAccess.GetPersonInfoByPersonID(PersonID, ref NationalNumber, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref Gender, ref DateOfBirth, ref Address, ref Phone, ref Email, ref ProfilePhotoPath,
            ref CountryID, ref CreatedByUser))
                return new Person(PersonID, NationalNumber, FirstName, SecondName, ThirdName, LastName,
                (enGender)Gender, DateOfBirth, Address, Phone, Email, ProfilePhotoPath, CountryID, CreatedByUser);

            return null;
        }
        public static Person Find(string NationalNumber)
        {
            int PersonID = ValidationConstants.INVALID_ID;
            string FirstName = string.Empty, SecondName = string.Empty, ThirdName = string.Empty, LastName = string.Empty;
            string Address = string.Empty, Phone = string.Empty, Email = string.Empty, ProfilePhotoPath = null;
            byte Gender = ((byte)enGender.Male);
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = ValidationConstants.INVALID_ID;
            int CreatedByUser = ValidationConstants.INVALID_ID;


            if (PersonDataAccess.GetPersonInfoByNationalNumber(NationalNumber, ref PersonID, ref FirstName, ref SecondName,
            ref ThirdName, ref LastName, ref Gender, ref DateOfBirth, ref Address, ref Phone, ref Email, ref ProfilePhotoPath, ref CountryID, ref CreatedByUser))
                return new Person(PersonID, NationalNumber, FirstName, SecondName, ThirdName, LastName,
                (enGender)Gender, DateOfBirth, Address, Phone, Email, ProfilePhotoPath, CountryID, CreatedByUser);

            return null;
        }
        private bool _AddNewPerson()
        {
            this.PersonID = PersonDataAccess.AddNewPerson(
                this.NationalNumber,
                this.FirstName,
                this.SecondName,
                this.ThirdName,
                this.LastName,
                (byte)this.Gender,
                this.DateOfBirth,
                this.Address,
                this.Phone,
                this.Email,
                this.ProfilePhotoPath,
                this.CountryInfo.CountryID,
                this.CreatedByUser
            );

            return (this.PersonID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdatePerson()
        {
            return PersonDataAccess.UpdatePerson(
                this.PersonID,
                this.NationalNumber,
                this.FirstName,
                this.SecondName,
                this.ThirdName,
                this.LastName,
                (byte)this.Gender,
                this.DateOfBirth,
                this.Address,
                this.Phone,
                this.Email,
                this.ProfilePhotoPath,
                this.CountryInfo.CountryID,
                this.CreatedByUser
            );
        }


        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewPerson())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;

                case enMode.Update:
                    return _UpdatePerson();
            }

            return false;
        }


        public static bool IsPersonExist(int PersonID)
        {
            return PersonDataAccess.IsPersonExist(PersonID);
        }
        public static bool IsPersonExist(string NationalNumber)
        {
            return PersonDataAccess.IsPersonExist(NationalNumber);
        }

        public static bool Delete(int PersonID)
        {
            return PersonDataAccess.DeletePerson(PersonID);
        }
    }
}

using System;
using System.Data;
using ContactsDataAccessLayer;

namespace ContactsBusinessLayer
{
    public class clsContact
    {
        public enum enMode { AddNew = 0, Update = 1 };


        public int ID { set; get; }
        public string FirstName { set; get; }
        public string LastName { set; get; }
        public string Email { set; get; }
        public string Phone { set; get; }
        public string Address { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string ImagePath { set; get; }
        public int CountryID { set; get; }
        public enMode Mode = enMode.AddNew;


        public clsContact() : this(-1, "", "", "", "", "", DateTime.Now, -1, "")
        {
            this.Mode = enMode.AddNew;
        }
        private clsContact(int ID, string FirstName, string LastName, string Email, string Phone, string Address, DateTime DateOfBirth, int CountryID, string ImagePath)
        {
            this.ID = ID;
            this.FirstName = FirstName;
            this.LastName = LastName;
            this.Email = Email;
            this.Phone = Phone;
            this.Address = Address;
            this.DateOfBirth = DateOfBirth;
            this.CountryID = CountryID;
            this.ImagePath = ImagePath;
            this.Mode = enMode.Update;
        }


        private bool _addNewContact()
        {
            //call DataAccess Layer 
            this.ID = clsContactDataAccess.addNewContact(
                this.FirstName,
                this.LastName,
                this.Email,
                this.Phone,
                this.Address,
                this.DateOfBirth,
                this.CountryID,
                this.ImagePath
            );

            return (this.ID != -1);
        }
        private bool _updateContact()
        {
            //call DataAccess Layer 
            return clsContactDataAccess.updateContact(
                this.ID,
                this.FirstName,
                this.LastName,
                this.Email,
                this.Phone,
                this.Address,
                this.DateOfBirth,
                this.CountryID,
                this.ImagePath
            );
        }


        public static DataTable getAllContacts()
        {
            return clsContactDataAccess.getAllContacts();
        }
        public static clsContact find(int ID)
        {
            string FirstName = "", LastName = "", Email = "", Phone = "", Address = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int CountryID = -1;

            if (clsContactDataAccess.getContactInfoByID(ID, ref FirstName, ref LastName, ref Email, ref Phone, ref Address, ref DateOfBirth, ref CountryID, ref ImagePath))
                return new clsContact(ID, FirstName, LastName, Email, Phone, Address, DateOfBirth, CountryID, ImagePath);

            return null;
        }
        public bool save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_addNewContact())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _updateContact();
            }

            return false;
        }
        public static bool deleteContact(int ID)
        {
            return clsContactDataAccess.deleteContact(ID);
        }
        public static bool isContactExist(int ID)
        {
            return clsContactDataAccess.isContactExist(ID);
        }
    }
}

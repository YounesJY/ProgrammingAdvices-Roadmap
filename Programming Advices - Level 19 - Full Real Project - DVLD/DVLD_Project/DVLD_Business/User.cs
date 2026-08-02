using System;
using System.Data;
using DVLD_DataAccess;
using DVLD_Common;

namespace DVLD_Business
{
    public class User
    {
        public enum enMode : byte { AddNew = 0, Update = 1 };
        private enMode _Mode = enMode.AddNew;

        public int UserID { get; private set; }
        public int PersonID { get; private set; }
        public Person PersonDetails { get; private set; }
        public string UserName { get; private set; }
        public string Password { get; private set; }
        public bool IsActive { get; private set; }


        private User(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonDetails = Person.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            this._Mode = enMode.Update;
        }
        public User() : this(ValidationConstants.INVALID_ID, ValidationConstants.INVALID_ID, string.Empty, string.Empty, false)
        {
            this._Mode = enMode.AddNew;
        }

        public static DataTable GetAllUsers()
        {
            return UserDataAccess.GetAllUsers();
        }

        public static User FindByUserID(int UserID)
        {
            int PersonID = ValidationConstants.INVALID_ID;
            string UserName = string.Empty, Password = string.Empty;
            bool IsActive = false;

            if (UserDataAccess.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive))
                return new User(UserID, PersonID, UserName, Password, IsActive);

            return null;
        }
        public static User FindByPersonID(int PersonID)
        {
            int UserID = ValidationConstants.INVALID_ID;
            string UserName = string.Empty, Password = string.Empty;
            bool IsActive = false;

            if (UserDataAccess.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive))
                return new User(UserID, PersonID, UserName, Password, IsActive);

            return null;
        }
        public static User FindByUsernameAndPassword(string UserName, string Password)
        {
            int UserID = ValidationConstants.INVALID_ID;
            int PersonID = ValidationConstants.INVALID_ID;
            bool IsActive = false;

            if (UserDataAccess.GetUserInfoByUsernameAndPassword(UserName, Password, ref UserID, ref PersonID, ref IsActive))
                return new User(UserID, PersonID, UserName, Password, IsActive);

            return null;
        }

        public static User create(int PersonID, string UserName, string Password, bool IsActive)
        {
            User user = new User();

            user.PersonID = PersonID;
            user.UserName = UserName;
            user.Password = Password;
            user.IsActive = IsActive;

            return user;
        }

        private bool _AddNewUser()
        {
            this.UserID = UserDataAccess.AddNewUser(
                this.PersonID,
                this.UserName,
                this.Password,
                this.IsActive
            );

            return (this.UserID != ValidationConstants.INVALID_ID);
        }
        private bool _UpdateUser()
        {
            return UserDataAccess.UpdateUser(
                this.UserID,
                this.PersonID,
                this.UserName,
                this.Password,
                this.IsActive
            );
        }
        public bool Save()
        {
            switch (_Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        _Mode = enMode.Update;
                        return true;
                    }
                    break;

                case enMode.Update:
                    return _UpdateUser();
            }

            return false;
        }

        public static bool IsUserExist(int UserID)
        {
            return UserDataAccess.IsUserExist(UserID);
        }
        public static bool IsUserExist(string UserName)
        {
            return UserDataAccess.IsUserExist(UserName);
        }
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return UserDataAccess.IsUserExistForPersonID(PersonID);
        }

        public static bool Delete(int UserID)
        {
            return UserDataAccess.DeleteUser(UserID);
        }

        public bool ChangePassword(string NewPassword)
        {
            if (UserDataAccess.ChangePassword(this.UserID, NewPassword))
            {
                this.Password = NewPassword;
                return true;
            }

            return false;
        }
    }
}

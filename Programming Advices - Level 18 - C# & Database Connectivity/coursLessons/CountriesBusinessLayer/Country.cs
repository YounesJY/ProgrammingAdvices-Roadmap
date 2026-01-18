using CountriesDataAccessLayer;
using System;
using System.Data;
using System.Data.SqlClient;


namespace CountriesBusinessLayer
{
    public class Country
    {
        public enum enMode { AddNew = 0, Update = 1 };

        public int CountryID { set; get; }
        public string CountryName { set; get; }
        public string Code { set; get; }
        public string PhoneCode { set; get; }

        public enMode Mode = enMode.AddNew;


        public Country() : this(-1, "", "", "")
        {
            Mode = enMode.AddNew;
        }
        private Country(int CountryID, string CountryName, string Code, string PhoneCode)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
            this.Code = Code;
            this.PhoneCode = PhoneCode;
            Mode = enMode.Update;
        }


        private bool _addNewCountry()
        {
            CountryID = CountryData.addNewCountry(CountryName, Code, PhoneCode);

            return (CountryID != -1);
        }
        private bool _updateCountry()
        {
            return CountryData.updateCountry(CountryID, CountryName, Code, PhoneCode);
        }


        public static DataTable getAllCountries()
        {
            return CountryData.getAllCountries();
        }
        public static Country find(int CountryID)
        {
            string CountryName = "", Code = "", PhoneCode = "";

            if (CountryData.getCountryInfoByID(CountryID, ref CountryName, ref Code, ref PhoneCode))
                return new Country(CountryID, CountryName, Code, PhoneCode);

            return null;
        }

        public static Country find(String CountryName)
        {
            int CountryID = -1;
            string Code = "", PhoneCode = "";

            if (CountryData.getCountryInfoByName(ref CountryID, ref CountryName, ref Code, ref PhoneCode))
                return new Country(CountryID, CountryName, Code, PhoneCode);

            return null;
        }


        public bool save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_addNewCountry())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                        return false;
                case enMode.Update:
                    return _updateCountry();
            }

            return false;
        }

        public static bool deleteCountry(int CountryID)
        {
            return CountryData.deleteCountry(CountryID);
        }

        public static bool isCountryExist(int CountryID)
        {
            return CountryData.isCountryExist(CountryID);
        }

        public static bool isCountryExist(string CountryName)
        {
            return CountryData.isCountryExist(CountryName);
        }
    }
}

using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using DVLD_Common;

namespace DVLD_Business
{
    public class Country
    {
        public int CountryID { set; get; }
        public string CountryName { set; get; }


        private Country(int ID, string CountryName)
        {
            this.CountryID = ID;
            this.CountryName = CountryName;
        }
        public Country() : this(ValidationConstants.INVALID_ID, null) { }


        public static DataTable getAllCountries()
        {
            return CountryData.GetAllCountries();
        }
        public static Country Find(int ID)
        {
            string CountryName = string.Empty;

            if (CountryData.GetCountryInfoByID(ID, ref CountryName))
                return new Country(ID, CountryName);

            return null;
        }
        public static Country Find(string CountryName)
        {
            int ID = ValidationConstants.INVALID_ID;

            if (CountryData.GetCountryInfoByName(CountryName, ref ID))
                return new Country(ID, CountryName);

            return null;
        }
    }
}

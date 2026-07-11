using DVLD_DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DVLD_Business.Person;

namespace DVLD_Business
{
    public class Country
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;


        public int ID { set; get; }
        public string CountryName { set; get; }


        private Country(int ID, string CountryName)
        {
            this.ID = ID;
            this.CountryName = CountryName;

            Mode = enMode.Update;
        }

        public Country() : this(-1, "")
        {
            Mode = enMode.AddNew;
        }


        public static DataTable getAllCountries()
        {
            return CountryDataAccess.GetAllCountries();
        }

        public static Country Find(int ID)
        {
            string CountryName = "";

            if (CountryDataAccess.GetCountryInfoByID(ID, ref CountryName))
                return new Country(ID, CountryName);

            return null;
        }

        public static Country Find(string CountryName)
        {
            int ID = -1;

            if (CountryDataAccess.GetCountryInfoByName(CountryName, ref ID))
                return new Country(ID, CountryName);

            return null;
        }

    }
}

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
        public int CountryID { set; get; }
        public string CountryName { set; get; }


        private Country(int ID, string CountryName)
        {
            this.CountryID = ID;
            this.CountryName = CountryName;
        }
        public Country() : this(-1, null) { }


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

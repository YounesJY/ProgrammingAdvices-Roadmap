using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DVLD.Classes
{
    public class Format
    {
        public static string DateToShort(DateTime dateTime)
        {
            
            return dateTime.ToString("dd/MMM/yyyy");
        } 

    }
}

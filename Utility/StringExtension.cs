using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public static class StringExtension
    {
        public static string GetDateString(this string Datevalue,string InputDateFormat= "dd/MM/yyyy",string OutputDateFormat= "MM/dd/yyyy")
        {
            return DateTime.ParseExact(Datevalue, InputDateFormat, CultureInfo.InvariantCulture).ToString(OutputDateFormat);
        }
    }
}

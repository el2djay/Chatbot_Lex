using Nager.Date;
using Nager.Date.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chatbot1
{
    public class Holiday
    {
        protected IEnumerable<PublicHoliday> publicHolidays;
        public IDictionary<string, DateTime> holidayDict;
        public int Year;

        //Year slot in lex
        public string TheYear { get; set; }  

        public string HolidayCalc(int year)
        {
           
            this.Year = year;
           
            if (Year == DateTime.Today.Year)
            {
                
                publicHolidays = DateSystem.GetPublicHoliday(DateTime.Today, new DateTime(DateTime.Today.Year, 12, 31), CountryCode.US);
            }

            else
            {
                publicHolidays = DateSystem.GetPublicHoliday(DateTime.Today.AddYears(1).Year, CountryCode.US);
            }

            //create dictionary with name and date of holiday

            holidayDict = new Dictionary<string, DateTime>();

            foreach (PublicHoliday holiday in publicHolidays)
            {
                if (holiday.Date >= DateTime.Today)
                {
                    holidayDict.TryAdd(holiday.LocalName, holiday.Date);
                }
            }

            // adjust fixed holidays in dictionary
            if (holidayDict.ContainsKey("New Year\'s Day"))
            {
                year = holidayDict["New Year\'s Day"].Year;
                holidayDict["New Year\'s Day"] = new DateTime(year, 1, 1);
            }
            if (holidayDict.ContainsKey("Independence Day"))
            {
                year = holidayDict["Independence Day"].Year;
                holidayDict["Independence Day"] = new DateTime(year, 7, 4);
            }
            if (holidayDict.ContainsKey("Veterans Day"))
            {
                year = holidayDict["Veterans Day"].Year;
                holidayDict["Veterans Day"] = new DateTime(year, 11, 11);
            }
            if (holidayDict.ContainsKey("Christmas Day"))
            {
                year = holidayDict["Christmas Day"].Year;
                holidayDict["Christmas Day"] = new DateTime(year, 12, 25);
            }

            var output = new StringBuilder();
          
            foreach (string key in holidayDict.Keys)
            {
                var dateLong = holidayDict[key].ToLongDateString();
                output.Append(key + ": "+"\n" + dateLong + "");
               
            }
            return output.ToString();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Utilities
{
    public static class Utility
    {

        public static string GetSeason()
        {
            
            float value = (float)DateTime.Now.Month + DateTime.Now.Day / 100f;  // <month>.<day(2 digit)>    
            if (value < 3.21 || value >= 12.22) return "Winter";   // Winter
            if (value < 6.21) return "Spring"; // Spring
            if (value < 9.23) return "Summer"; // Summer
            return "Autumn";   // Autumn
        }
        public static string GetPartofDay()
        {
            //Depending on the current time, print either "Good morning," "Good afternoon," or "Good evening"
            DateTime time = new DateTime();

            //get the current time
            time = DateTime.Now;
            string timeValue = string.Empty;
            //if it is past midnight but before noon, print "Good morning."
            if (time.Hour >= 0 && time.Hour < 12)
            {
                timeValue = "Morning";
            }
            //if it is past noon but before 6PM, print "Good afternoon."
            else if (time.Hour >= 12 && time.Hour < 18)
            {
                timeValue = "Afternoon";
            }
            //if it is past 6PM, print "Good evening."
            else if (time.Hour >= 18)
            {
                timeValue = "Evening";
            }
            return timeValue;
        }
    }
}

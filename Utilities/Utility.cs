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
            
            float value = (float)DateTime.Now.Month + DateTime.Now.Day / 100f; 
            if (value < 3.21 || value >= 12.22) return "Winter"; 
            if (value < 6.21) return "Spring";
            if (value < 9.23) return "Summer"; 
            return "Autumn";  
        }
        public static string GetPartofDay()
        {
            DateTime time = new DateTime();

            time = DateTime.Now;
            string timeValue = string.Empty;
           
            if (time.Hour >= 0 && time.Hour < 12)
            {
                timeValue = "Morning";
            }
          
            else if (time.Hour >= 12 && time.Hour < 18)
            {
                timeValue = "Afternoon";
            }
         
            else if (time.Hour >= 18)
            {
                timeValue = "Evening";
            }
            return timeValue;
        }
    }
}

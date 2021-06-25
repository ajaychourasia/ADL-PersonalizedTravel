using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Models
{
    public class UserContext
    {
        public bool UseUserAgent { get; set; }

        //public string TripType { get; set; }

        public string TravelerHistory { get; set; }

        public string Device { get; set; }

        public string Country { get; set; }

        public string Gender { get; set; }

        public string TripPreference{ get; set; }

        public string PartofDay { get; set; }

        public string Season { get; set; }
       
    }
}

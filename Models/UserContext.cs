using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Models
{
    public class UserContext
    {
        public bool UseUserAgent { get; set; }

        public string tripType { get; set; }

        public string TravelerHistory { get; set; }

        public string Device { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public DateTime time { get; set; }
       
    }
}

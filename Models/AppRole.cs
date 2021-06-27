using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ADL.PersonalizedTravel.Models
{
    public class AppUser : IdentityUser
    {
           
        [DataType(DataType.Text)]
        public string TripPreference { get; set; }

        [DataType(DataType.Text)]
        public string Gender { get; set; }

        [DataType(DataType.Text)]
        public string Country { get; set; }

    }
}


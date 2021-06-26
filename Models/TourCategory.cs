using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Models
{
   
    public class TourCategory
    {
        public TourCategory()
        {
            Enabled = true;
        }

        public string Id { get; set; }

        public bool Enabled { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

      
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Models
{
    public class TourActivity
    {
        public string TourCategoryId { get; set; }
        public string Id { get; set; }

        public bool Enabled { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }
    }
}

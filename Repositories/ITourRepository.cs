using ADL.PersonalizedTravel.Models;
using Microsoft.Azure.CognitiveServices.Personalizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Repositories
{
    public interface ITourRepository
    {
        IList<TourCategory> GetTour();
        TourCategory GetTour(string id);

        List<TourActivity> GetTourActivity(string id);
    }
}

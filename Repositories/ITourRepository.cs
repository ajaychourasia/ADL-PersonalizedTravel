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
        IList<TourDetail> GetTour();
        TourDetail GetTour(string id);
    }
}

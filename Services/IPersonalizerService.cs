using Microsoft.Azure.CognitiveServices.Personalizer.Models;
using ADL.PersonalizedTravel.Models;
using System.Collections.Generic;


namespace ADL.PersonalizedTravel.Services
{
    public interface IPersonalizerService
    {
        RankResponse GetTourRecommendations(IList<object> context);
        IList<Action> GetRankedTour(IList<object> context);
        void Reward(Reward reward);
    }
}

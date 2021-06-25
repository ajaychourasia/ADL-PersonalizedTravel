using Microsoft.Azure.CognitiveServices.Personalizer;
using Microsoft.Azure.CognitiveServices.Personalizer.Models;
using ADL.PersonalizedTravel.Models;
using ADL.PersonalizedTravel.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ADL.PersonalizedTravel.Services
{
    public class PersonalizerService : IPersonalizerService
    {
        private readonly IPersonalizerClient _personalizerClient;
        private readonly IRankableActionRepository _actionsRepository;
        private readonly IActionRepository _actionRepository;

        public PersonalizerService(IRankableActionRepository actionsRepository, IPersonalizerClient personalizerClient, IActionRepository actionRepository)
        {
            _actionsRepository = actionsRepository;
            _personalizerClient = personalizerClient;
            _actionRepository = actionRepository;
        }

        public RankResponse GetTourRecommendations(IList<object> context)
        {
            var eventId = Guid.NewGuid().ToString();
            var actions = _actionsRepository.GetActions();
            //get Details Age ,gender, Trip and paas in Reuest
            var request = new RankRequest(actions, context, null, eventId);
            RankResponse response = _personalizerClient.Rank(request);
            return response;
        }

        public IList<Models.Action> GetRankedTour(IList<object> context)
        {
            var recommendations = GetTourRecommendations(context).Ranking.Select(x => x.Id).ToList();
            var actions = _actionRepository.GetActions();

            return actions.OrderBy(action => recommendations.IndexOf(action.Id)).ToList();
        }

        public void Reward(Reward reward)
        {
            _personalizerClient.Reward(reward.EventId, new RewardRequest(reward.Value));
        }
    }
}

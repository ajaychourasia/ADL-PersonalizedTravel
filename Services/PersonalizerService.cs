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
        /// <summary>
        /// Get User tour recommendations based on Previous actions
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public RankResponse GetTourRecommendations(IList<object> context)
        {
            RankResponse response = null;
            var eventId = Guid.NewGuid().ToString();
            var actions = _actionsRepository.GetActions();
            var request = new RankRequest(actions, context, null, eventId);
            try
            {
                 response = _personalizerClient.Rank(request);
            }
            catch (Exception ex)
            {
                //Log excetion
                //[NOTE: Throwing an Exception is being ignored to execute default workflow for Demo purpose]
            }

            return response;
        }

        /// <summary>
        /// Get user ranked based on Tour actions 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public IList<Models.Action> GetRankedTour(IList<object> context)
        {
            var recommendations = GetTourRecommendations(context).Ranking.Select(x => x.Id).ToList();
            var actions = _actionRepository.GetActions();

            return actions.OrderBy(action => recommendations.IndexOf(action.Id)).ToList();
        }

        /// <summary>
        /// Return Rewards 
        /// </summary>
        /// <param name="reward"></param>
        public void Reward(Reward reward)
        {
            _personalizerClient.Reward(reward.EventId, new RewardRequest(reward.Value));
        }
    }
}

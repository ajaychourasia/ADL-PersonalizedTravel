using Microsoft.Azure.CognitiveServices.Personalizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Models
{
    public class RankableActionWithMetadata : RankableAction
    {
        public RankableActionWithMetadata(Action action)
        {
            Id = action.Id;
            Features = new List<object>()
            {
                new {action.Title},
                new {action.Image}
                
            };

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADL.PersonalizedTravel.Models;
using Microsoft.Azure.CognitiveServices.Personalizer.Models;

namespace ADL.PersonalizedTravel.Repositories
{
    public interface IRankableActionRepository
    {
        IList<RankableAction> GetActions();

        IList<RankableActionWithMetadata> GetActionsWithMetadata();
    }
}

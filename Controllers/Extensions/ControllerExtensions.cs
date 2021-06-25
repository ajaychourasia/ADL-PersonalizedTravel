
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ADL.PersonalizedTravel.Models;
using System.Collections.Generic;

namespace ADL.PersonalizedTravel.Controllers.Extensions
{
    public static class ControllerExtensions
    {
        //try changing name
        public static IList<object> CreatePersonalizerContext(this Controller controller, UserContext context, HttpRequest request)
        {
            var result = new List<object>
            {
                new {context.Device,
                    //context.TripType,
                    context.TravelerHistory,
                    context.Gender,
                    context.TripPreference,
                    context.Country,
                    context.PartofDay,
                    context.Season
                }
            };

            if (request != null)
            {
                var userAgent = new UserAgentInfo();
                userAgent.UseUserAgent(request.Headers["User-Agent"]);
                result.Add(userAgent);
            }

            return result;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ADL.PersonalizedTravel.Utilities
{
    public static class AppInsightsHelper
    {
        public static void TrackException(Exception exc)
        {

        }
       


        public static void TrackEvent(string eventName, Dictionary<string, string> properties)
        {
            var tc = new Microsoft.ApplicationInsights.TelemetryClient();
            tc.TrackEvent(eventName,properties);

        }

        public static void TrackPageView(string eventName) //TelemetryClient(TelemetryConfiguration.CreateDefault());
        {
            //var tc = new Microsoft.ApplicationInsights.TelemetryClient(TelemetryConfiguration.CreateDefault());
           // tc.TrackPageView(eventName);

        }

        public static void SetUserContext()
        {
            var tc = new Microsoft.ApplicationInsights.TelemetryClient();
           
        }
    }
}

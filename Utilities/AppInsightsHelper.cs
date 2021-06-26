using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Extensibility;
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
            //var tc = new TelemetryClient();
            //tc.TrackEvent(eventName,properties);

        }

        public static void TrackPageView(string eventName)
        {
            TelemetryConfiguration configuration = TelemetryConfiguration.CreateDefault();
            configuration.InstrumentationKey = "";
            var telemetryClient = new TelemetryClient(configuration);

            telemetryClient.TrackPageView(eventName);

            // before exit, flush the remaining data
            telemetryClient.Flush();

            Task.Delay(5000).Wait();
        }

        public static void SetUserContext()
        {
           
        }
    }
}

using ADL.PersonalizedTravel.Models;
using Microsoft.AspNetCore.Mvc;
using ADL.PersonalizedTravel.Services;
using ADL.PersonalizedTravel.Controllers.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using ADL.PersonalizedTravel.Utilities;
using ADL.PersonalizedTravel.Repositories;
using System.Data.SqlClient;
using Microsoft.ApplicationInsights;

namespace ADL.PersonalizedTravel.Controllers
{
    //[Route("api/[controller]")]
    public class TourController : Controller
    {

        private readonly IPersonalizerService _service;
        private readonly ITourRepository _tourRepository;
        private TelemetryClient _telemetry;
        public UserManager<AppUser> _userManager { get; set; }
        public string tourId = string.Empty;

        public TourController(IPersonalizerService service, UserManager<AppUser> userManager , ITourRepository tourRepository ,TelemetryClient telemetry)
        {
            _service = service;
            _userManager = userManager;
            _tourRepository = tourRepository;
            _telemetry = telemetry;
        }
      
        //[Route("Tour/Recommendation")]
        public async Task<JsonResult> Recommendation([FromBody] UserContext context)
        {
            
            //if user login Fill details 
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                var appUser = await _userManager.GetUserAsync(User);
                context.Gender = appUser.Gender;
                context.TripPreference = appUser.TripPreference;
                context.Country = appUser.Country;
            }
            //Get User default data irrespective of login
            context.PartofDay = Utility.GetPartofDay();
            context.Season = Utility.GetSeason();
            
            var currentContext = this.CreatePersonalizerContext(context, context.UseUserAgent ? Request : null);

            return new JsonResult(_service.GetTourRecommendations(currentContext));
        }

        public JsonResult GetTour([FromBody] TourCategory tour)
        {
            tourId = tour.Id;
            var model = _tourRepository.GetTour(tour.Id);
             return new JsonResult(model);
        }

        //[Route("Tour/Reward")]
        public void Reward([FromBody] Reward reward)
        {
            _service.Reward(reward);
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetPersonalizedTourActivities(TourCategory tour)
        {     
            var model = _tourRepository.GetTourActivity("3");
            return new JsonResult(model);
        }

        public int GetCluster()
        {
            int clusterId = 0;
            string SqlConnection = "Server=tcp:traveldemosqlserver.database.windows.net,1433;Initial Catalog=traveldemoapp;Persist Security Info=False;User ID=azureuser;Password={*****}; MultipleActiveResultSets =False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (var connection = new SqlConnection(SqlConnection))
            {
                var sql = "SELECT Assignments FROM traveldemoclusters where UserID = 'nrDJJrO7R9+wXSYKT8y6Sz'";
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        // clusterId = (int)reader["Assignments"];
                       
                    }
                    return clusterId;
                }
                catch (Exception ex)
                {

                    throw;
                }
               
            }
        }
    }
}
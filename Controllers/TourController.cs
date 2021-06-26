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

        public JsonResult GetPersonalizedTourActivities()
        {
            string userId = string.Empty;
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value;
            }
            else
            {
                if(Request.Cookies.ContainsKey("ai_user"))
                {
                    userId = Request.Cookies["ai_user"].Split("|")[0];
                }
            }
            var clusterId = GetCluster(userId);
            var model = _tourRepository.GetTourActivity(clusterId);
            return new JsonResult(model);
        }

        public string GetCluster(string userId)
        {
            string clusterId = "1";
            string SqlConnection = "Server=tcp:traveldemosqlserver.database.windows.net,1433;Initial Catalog=traveldemoapp;Persist Security Info=False;User ID=azureuser;Password={*****}; MultipleActiveResultSets =False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            using (var connection = new SqlConnection(SqlConnection))
            {
                string column = string.Empty;
                if (User?.Identity?.IsAuthenticated ?? false)
                    column = "UserAuthenticatedId";
                else
                    column = "UserId";

                //TODO: remove this code, just for testing
                userId = "nrDJJrO7R9+wXSYKT8y6Sz";

                var sql = "SELECT TOP(1) * FROM traveldemoclusters where " + column + " = @userId";
                try
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand(sql, connection);
                    command.Parameters.Add(new SqlParameter("@userId", userId));
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read())
                    {
                        clusterId = Convert.ToString(reader["Assignments"]);                       
                    }
                    return clusterId == "0" ? "1" : clusterId;
                }
                catch (Exception ex)
                {
                    throw;
                }
               
            }
        }
    }
}
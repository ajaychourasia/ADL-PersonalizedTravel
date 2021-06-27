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
        public string tourActivityId = string.Empty;

        public TourController(IPersonalizerService service, UserManager<AppUser> userManager , ITourRepository tourRepository ,TelemetryClient telemetry)
        {
            _service = service;
            _userManager = userManager;
            _tourRepository = tourRepository;
            _telemetry = telemetry;
        }
      
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

        public JsonResult GetTourCategory([FromBody] TourCategory tour)
        {
            tourId = tour.Id;
            var model = _tourRepository.GetTourCategory(tour.Id);
            return new JsonResult(model);
        }
        public IActionResult TourCategoryDetail(string id)
        {
            var model = _tourRepository.GetTourCategory(id);
            AppInsightsHelper.TrackPageView(model.Title); 
            ViewData["Title"] = model.Title;
            return View(model);
        }

        public IActionResult TourActivityDetail(string id)
        {
            tourId = id;
            var model = _tourRepository.GetTourActivityDetail(tourId);
            return View(model);
           
        }

        public void Reward([FromBody] Reward reward)
        {
            _service.Reward(reward);
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
            DbQuery dbQuery = new DbQuery();
            string clusterId = dbQuery.GetDbResultSet(userId);
            return clusterId;
        }

        public IActionResult Index()
        {
            return View();
        }

    }
}
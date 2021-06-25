using ADL.PersonalizedTravel.Models;
using Microsoft.AspNetCore.Mvc;
using ADL.PersonalizedTravel.Services;
using ADL.PersonalizedTravel.Controllers.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using ADL.PersonalizedTravel.Utilities;
using ADL.PersonalizedTravel.Repositories;

namespace ADL.PersonalizedTravel.Controllers
{
    //[Route("api/[controller]")]
    public class TourController : Controller
    {

        private readonly IPersonalizerService _service;
        private readonly ITourRepository _tourRepository;
        public UserManager<AppUser> _userManager { get; set; }

        public TourController(IPersonalizerService service, UserManager<AppUser> userManager , ITourRepository tourRepository)
        {
            _service = service;
            _userManager = userManager;
            _tourRepository = tourRepository;
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

        public JsonResult GetTour([FromBody] string id)
        {
            var model = _tourRepository.GetTour(id);
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
    }
}
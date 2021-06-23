using ADL.PersonalizedTravel.Models;
using Microsoft.AspNetCore.Mvc;
using ADL.PersonalizedTravel.Services;
using ADL.PersonalizedTravel.Controllers.Extensions;

namespace ADL.PersonalizedTravel.Controllers
{
    //[Route("api/[controller]")]
    public class TourController : Controller
    {

        private readonly IPersonalizerService _service;

        public TourController(IPersonalizerService service)
        {
            _service = service;
        }
      
        //[Route("Tour/Recommendation")]
        public JsonResult Recommendation([FromBody] UserContext context)
        {
            var currentContext = this.CreatePersonalizerContext(context, context.UseUserAgent ? Request : null);

            return new JsonResult(_service.GetTourRecommendations(currentContext));
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
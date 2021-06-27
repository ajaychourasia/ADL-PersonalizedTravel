using System.Collections.Generic;
using System.Linq;
using ADL.PersonalizedTravel.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;


namespace ADL.PersonalizedTravel.Repositories
{
    public class TourRepository : ITourRepository
    {
        private IList<TourCategory> _tours;
        private IList<TourActivity> _activity;

        public TourRepository(IHostingEnvironment hostingEnvironment)
        {
            var fileProvider = hostingEnvironment.ContentRootFileProvider;
            var contents = fileProvider.GetDirectoryContents("TourCategories");
            _tours = contents
                            .Select(file => System.IO.File.ReadAllText(file.PhysicalPath))
                            .Select(fileContent => JsonConvert.DeserializeObject<TourCategory>(fileContent))
                            .Where(a => a.Enabled)
                            .ToList();

            var activityContents = fileProvider.GetDirectoryContents("TourActivities");

            _activity = activityContents
                            .Select(file => System.IO.File.ReadAllText(file.PhysicalPath))
                            .Select(fileContent => JsonConvert.DeserializeObject<TourActivity>(fileContent))
                            .Where(a => a.Enabled)
                            .ToList();
            
        }

        public List<TourActivity> GetTourActivity(string tourId)
        {
            return _activity.Where(activity => tourId == activity.TourCategoryId).ToList();
        }

        public TourActivity GetTourActivityDetail(string tourId)
        {
            return _activity.FirstOrDefault(activity => tourId == activity.Id);
        }
        public TourCategory GetTourCategory(string id)
        {
            return _tours.FirstOrDefault(tours => tours.Id == id);
        }

        public IList<TourCategory> GetTour()
        {
            return _tours.ToList();
        }
    }
}

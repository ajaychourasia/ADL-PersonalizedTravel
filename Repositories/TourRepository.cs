using System.Collections.Generic;
using System.Linq;
using ADL.PersonalizedTravel.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;


namespace ADL.PersonalizedTravel.Repositories
{
    public class TourRepository : ITourRepository
    {
        private IList<TourDetail> _tours;

        public TourRepository(IHostingEnvironment hostingEnvironment)
        {
            var fileProvider = hostingEnvironment.ContentRootFileProvider;
            var contents = fileProvider.GetDirectoryContents("toursDetail");
            _tours = contents
                            .Select(file => System.IO.File.ReadAllText(file.PhysicalPath))
                            .Select(fileContent => JsonConvert.DeserializeObject<TourDetail>(fileContent))
                            .Where(a => a.Enabled)
                            .ToList();
        }

        public TourDetail GetTour(string id)
        {
            return _tours.FirstOrDefault(tours => tours.Id == id);
        }

        public IList<TourDetail> GetTour()
        {
            return _tours.ToList();
        }
    }
}

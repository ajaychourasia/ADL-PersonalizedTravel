using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using ADL.PersonalizedTravel.Models;

namespace ADL.PersonalizedTravel.Repositories
{
    public class ActionRepository : IActionRepository
    {
        private IList<Action> _actions;

        public ActionRepository(IHostingEnvironment hostingEnvironment)
        {
            //var fileProvider = hostingEnvironment.ContentRootFileProvider;
            //var fileContent = System.IO.File.ReadAllText(fileProvider.GetFileInfo("Actions/actions.json").PhysicalPath);
            //_actions = JsonConvert.DeserializeObject<List<Action>>(fileContent).Where(a => a.Enabled).ToList<Action>();
            _actions = new List<Action>
            {
                new Action {Id ="1", Title="Trek" ,Image = "https://cdn.pixabay.com/photo/2020/10/12/19/10/mountaineers-5649828_960_720.jpg"},
                new Action {Id ="2", Title="Camping" ,Image = "https://cdn.pixabay.com/photo/2019/06/28/03/07/camping-4303357_960_720.jpg"},
                new Action {Id ="3", Title="RoadTrip" ,Image = "https://cdn.pixabay.com/photo/2019/04/04/09/11/cycling-4102251_960_720.jpg"},
                new Action {Id ="4", Title="Beach" ,Image = "https://cdn.pixabay.com/photo/2016/03/04/19/36/beach-1236581__340.jpg" },
                new Action {Id ="5", Title="BagPacking" ,Image = "https://cdn.pixabay.com/photo/2016/03/26/22/16/nature-1281574_960_720.jpg"}
            };

        }

        public Action GetAction(string id)
        {
            return _actions.FirstOrDefault(action => action.Id == id);
        }

        public IList<Action> GetActions()
        {
            return _actions.ToList();
        }
    }
}

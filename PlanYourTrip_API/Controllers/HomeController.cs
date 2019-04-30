using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace PlanYourTrip_API.Controllers
{
    public class HomeController : ApiController
    {
        /*public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }*/

        // Get top rated packages
        [HttpGet]
        [Route("api/home/getTop")]
        public IQueryable GetTopPackages()
        {
            PackageManager packageManager = new PackageManager();
            
            return packageManager.GetTopPackages();
        }

        // Search function to search packages
        [HttpGet]
        [Route("api/home/search/{term}")]
        public IQueryable SearchPackages(string term)
        {
            PackageManager packageManager = new PackageManager();

            return packageManager.SearchPackages(term);
        }

        [HttpGet]
        [Route("api/home/getSuggested/{userName}")]
        public IEnumerable<PlanYourTripDataAccessLayer.PackageDetails> GetSuggestedPackages(string userName)
        {
            PackageManager packageManager = new PackageManager();

            return packageManager.GetSuggestedPackages(userName);
        }

        [HttpGet]
        [Route("api/home/getCategories")]
        public IEnumerable<string> GetCategories()
        {
            PackageManager packageManager = new PackageManager();

            return packageManager.GetCategories();
        }
    }
}

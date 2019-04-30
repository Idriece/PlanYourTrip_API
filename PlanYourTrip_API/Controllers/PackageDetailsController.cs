using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PlanYourTrip_API.Controllers
{
    public class PackageDetailsController : ApiController
    {
        readonly PackageManager packageManager = new PackageManager();

        // Return all packages 
        [HttpGet]
        [Route("api/packages/getAll")]
        public IQueryable GetPackages()
        {
            return packageManager.GetAllPackages();
        }

        // Get all itineraries for a certain package
        [HttpGet]
        [Route("api/packages/getItinerary/{packageID}")]
        public IQueryable GetItineraries(int packageID)
        {
            return packageManager.GetItinerary(packageID);
        }

        [HttpGet]
        [Route("api/packages/GetCustomItinerary/{packageID}")]
        public IQueryable GetCustomItineraries(int packageID)
        {
            return packageManager.GetCustomItineraries(packageID);
        }

        // Return all details of a particular package
        [HttpGet]
        [Route("api/packages/getFeedback/{packageID}")]
        public IQueryable GetFeedback(int packageID)
        {
            return packageManager.GetFeedback(packageID);
        }

        // Search function for package
        [HttpGet]
        [Route("api/packages/search/{term}")]
        public IQueryable SearchPackages(string term)
        {
            return packageManager.SearchPackages(term);
        }

        // Return all details for particular package
        [HttpGet]
        [Route("api/packages/getPackage/{packageID}")]
        public IQueryable GetPackage(int packageID)
        {
            return packageManager.GetPackage(packageID);
        }

        [HttpGet]
        [Route("api/packages/getRoomOptions/{packageID}")]
        public IQueryable GetRoomOptions(int packageID)
        {
            return packageManager.GetRoomOptions(packageID);
        }

        [HttpGet]
        [Route("api/packages/getTransportOptions/{packageID}")]
        public IQueryable GetTransportOptions(int packageID)
        {
            return packageManager.GetTransportOptions(packageID);
        }

        [HttpPost]
        [Route("api/packages/AddFeedback")]
        public void AddReview([FromBody] FeedBack feedBack)
        {
            FeedbackManager feedbackManager = new FeedbackManager();

            feedbackManager.AddFeedback(feedBack);
        }
    }
}

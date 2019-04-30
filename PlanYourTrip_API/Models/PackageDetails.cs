using PlanYourTripBusinessEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class PackageDetails
    {
        public int PackageID { get; set; }
        public string PackageType { get; set; }
        public string PackageName { get; set; }
        public int Days { get; set; }
        public int ProfitPercentage { get; set; }
        public decimal Price { get; set; }
        public string Summary { get; set; }
        public int NumberAvailable { get; set; }
        public int MinPeople { get; set; }
        public int MaxPeople { get; set; }
        public string Image { get; set; }
        public decimal Rating { get; set; }
        public IQueryable<Itinerary> Itineraries { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    public class CountDTO
    {
        public int? UserCount { get; set; }
        public int? PackageCount { get; set; }
        public int? HotelCount { get; set; }
        public int? TransportationCount { get; set; }
        public int? BookingCount { get; set; }
        public double? AverageProfit { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
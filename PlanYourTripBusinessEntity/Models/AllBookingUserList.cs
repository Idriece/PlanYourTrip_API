using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessEntity.Models
{
    public class AllBookingUserList
    {
        public int? RefundPercentage { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public int? NumberOfTrips { get; set; }
        public int? PackageBookingId { get; set; }
        public string PaymentMethod { get; set; }
        public int? PackageId { get; set; }
        public decimal? TotalPackageCost { get; set; }
        public string PackageName { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string PackageType { get; set; }
        public string BookingStatus { get; set; }
        public decimal? RefundAmount { get; set; }
        public string Summary { get; set; }
        public int? Days { get; set; }
        public string ActivityDetails { get; set; }
        public int? DayNumber { get; set; }
        public bool IsCustomized { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class ListAllUserBookingList
    {

        public string Email { get; set; }
        public int? NumberOfTrips { get; set; }
        public string Role { get; internal set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public List<StartDateDTO> StartDate { get; set; }
        public List<EndDateDTO> EndDate { get; set; }
        public List<PaymentMethodDTO> PaymentMethod { get; set; }
        public List<SummaryDTO> Summary { get; set; }
        public List<TotalCostDTO> TotalCost { get; set; }
        public List<BookingStatusDTO> BookingStatus { get; set; }
        public List<RefundAmountDTO> RefundAmount { get; set; }
        public List<RefundPercentageDTO> RefundPercentage { get; set; }
        public List<PackageNameDTO> PackageName { get; set; }
        public List<PackageTypesDTO> PackageType { get; set; }
        public List<ActivityDetailsDTO> ActivityDetails { get; set; }
        public List<DayNumberDTO> DayNumber { get; set; }
    }   
        

        public class SummaryDTO
        {
            public string Summary { get; set; }
        }
        public class ActivityDetailsDTO
        {
            public string ActivityDetails { get; set; }
        }
        public class PaymentMethodDTO
        {
            public string PaymentMethod { get; set; }
        }
        public class DayNumberDTO
        {
            public int? DayNumber { get; set; }
        }
        public class RefundPercentageDTO
        {
            public int? RefundPercentage { get; set; }
        }
        public class PackageNameDTO
        {
            public string PackageName { get; set; }
        }
        public class PackageTypesDTO
        {
            public string PackageType { get; set; }
        }
        public class BookingStatusDTO
        {
            public string BookingStatus { get; set; }
        }
        public class TotalCostDTO
        {
            public decimal? TotalCost { get; set; }
        }
        public class RefundAmountDTO
        {
            public decimal? RefundAmount { get; set; }
        }
        public class StartDateDTO
        {
            public DateTime? StartDate { get; set; }
        }
        public class EndDateDTO
        {
            public DateTime? EndDate { get; set; }
        }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlanYourTrip_API.Models
{
    public class Booking
    {
        public int PackageID { get; set; }
        public string UserName { get; set; }
        public int NumPeople { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string PaymentMethod { get; set; }
        public bool IsCustomized { get; set; }
        public decimal TotalCost { get; set; }
        public string BookingStatus { get; set; }
    }
}
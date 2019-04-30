using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_API.Models
{
    public class ListAllUserDetails
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<InterestDTO> Interests { get; set; }
        public string LoginProvider { get; set; }
        public List<PackageDTO> Packages { get; set; }
        public List<PackageTypeDTO> PackageType { get; set; }
        public string City { get; set; }
        
        public string State { get; set; }
        public string Email { get; set; }
        public int NumberOfTrips { get; set; }
        public string Role { get; internal set; }
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class InterestDTO
    {
        public int UserInterestID { get; set; }
        public string UserInterest { get; set; }
    }

    public class PackageDTO
    {
        public string PackageName { get; set; }
    }
    public class PackageTypeDTO
    {
        public string PackageTypes { get; set; }
    }
}

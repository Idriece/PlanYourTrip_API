using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessEntity.Models
{
    public class UserProfileDTO
    {
        public string UserName { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public List<Interests> Interest { get; set; } 
    }
    public class Interests
    {
        public string intereset { get; set; }
    }
}

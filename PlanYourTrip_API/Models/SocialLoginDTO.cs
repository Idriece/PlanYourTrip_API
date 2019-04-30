using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTrip_API
{
    // DTO for user info getting from google
    public class SocialLoginDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Id { get; set; }
        public string authToken { get; set; }
        public string IdToken { get; set; }
        public string Provider { get; set; }
        public string PhotoUrl { get; set; }
        public int CityId { get; set; }

    }
}

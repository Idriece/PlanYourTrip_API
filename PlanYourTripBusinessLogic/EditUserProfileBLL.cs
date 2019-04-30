using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessLogic
{
    
    public class EditUserProfileBLL
    {
        EditUserProfileDAL ep = new EditUserProfileDAL();
        public void SetUserProfile(string id, UserProfileDTO userprofiledto)
        {
            ep.EditUserProfileDal(id, userprofiledto);
        }
    }
}

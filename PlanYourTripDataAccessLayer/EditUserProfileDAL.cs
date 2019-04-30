using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    public class EditUserProfileDAL
    {
        PlanYourTripData db = new PlanYourTripData();
        public void EditUserProfileDal(string id, UserProfileDTO userprofiledto)
        {
            userprofiledto.UserId = id;
            db.Entry(userprofiledto).State = EntityState.Modified;
            db.SaveChanges();

        }
    }
}

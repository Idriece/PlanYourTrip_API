using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    public class FeedbackManager
    {
        readonly PlanYourTripData db = new PlanYourTripData();

        public void AddFeedback(FeedBack feedback)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            string Id = userManager.FindByName(feedback.Id).Id;
            feedback.Id = Id;

            FeedBack existingFeedback = (from fb in db.FeedBacks
                                         where fb.Id == feedback.Id && fb.PackageID == feedback.PackageID
                                         select fb).ToList()[0];

            if(feedback.Id == existingFeedback.Id && feedback.PackageID == existingFeedback.PackageID)
            {
                existingFeedback.Rating = feedback.Rating;
                existingFeedback.Review = feedback.Review;
                db.Entry(db.FeedBacks.Find(existingFeedback.FeedBackID)).Property("Review").IsModified = true;
                db.Entry(db.FeedBacks.Find(existingFeedback.FeedBackID)).Property("Rating").IsModified = true;
                db.SaveChanges();
            }
            else
            {
                db.FeedBacks.Add(feedback);
                db.SaveChanges();
            }
        }
    }
}

using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    public class AdminUserManager
    {

        readonly string AllUserDetailString =
            "select RefundPercentage, FirstName, LastName, Email, UserName, [User].Id as UserId, NumberOfTrips, PackageBooking.PackageBookingID as PackageBookingId, PaymentMethod, PackageBooking.PackageID as PackageId, PackageBooking.TotalCost as TotalPackageCost, PackageName, StartDate, EndDate, PackageType.Name as PackageType, BookingStatus, Refunds.RefundAmount as RefundAmount, Package.Summary as Summary, Package.Days as Days, Package.Summary as ActivityDetails, Package.Days as DayNumber, IsCustomized " +
            "from [User] " +
            "join PackageBooking on PackageBooking.Id = [User].Id " +
            "left join Package on PackageBooking.PackageID = Package.PackageID " +
            "left join PackageType on Package.PackageTypeID = PackageType.PackageTypeID " +
            "left join Refunds on PackageBooking.PackageBookingID = Refunds.PackageBookingID " +
            "left join RefundsRules on RefundsRules.RefundRuleID = Refunds.RefundRuleID;";

        readonly PlanYourTripData db = new PlanYourTripData();
        List<CountDTO> count = new List<CountDTO>();
        /// <summary>
        /// Function to return a count of Registered user, Package, Hotel owner, Transport Provider.
        /// </summary>
        /// <returns></returns>
        public List<CountDTO> Count()
        {
            count.Add(new CountDTO()
            {
                AverageProfit = db.Packages.Average(x => x.ProfitPercentage),
                TotalPrice = db.Packages.Sum(x => x.Price),
                UserCount = db.Users.Count(),
                PackageCount = db.Packages.Count(),
                BookingCount = db.PackageBookings.Count(),
                TransportationCount = db.TransportationProviders.Count(),
                HotelCount = db.Hotels.Count()
            });
            return count;
        }
        /// <summary>
        /// Function to perform a join operation of 10 tables to get a list of user details of user
        /// </summary>
        /// <returns></returns>

        public List<AllUserDetails> AllUserDetails()
        {

            var alluserdetails = (from user in db.Users
                                  join interset in db.UserInterests on user.Id equals interset.Id into val1
                                  from interset in val1.DefaultIfEmpty()
                                  join login in db.UserLogin on user.Id equals login.UserId into val2
                                  from login in val2.DefaultIfEmpty()
                                  join roletemp in db.UserRole on user.Id equals roletemp.UserId into val3
                                  from roletemp in val3.DefaultIfEmpty()
                                  join role in db.Role on roletemp.RoleId equals role.Id into val4
                                  from role in val4.DefaultIfEmpty()
                                  join packagetemp in db.PackageBookings on user.Id equals packagetemp.Id into val5
                                  from packagetemp in val5.DefaultIfEmpty()
                                  join packagename in db.Packages on packagetemp.PackageID equals packagename.PackageID into val6
                                  from packagename in val6.DefaultIfEmpty()
                                  join packagetype in db.PackageTypes on packagename.PackageTypeID equals packagetype.PackageTypeID into val7
                                  from packagetype in val7.DefaultIfEmpty()
                                  join city in db.Cities on user.CityID equals city.CityID into val8
                                  from city in val8.DefaultIfEmpty()
                                  join state in db.States on city.StateID equals state.StateID into val9
                                  from state in val9.DefaultIfEmpty()
                                  join refunds in db.Refunds on packagetemp.PackageBookingID equals refunds.PackageBookingID into val10
                                  from refunds in val10.DefaultIfEmpty()
                                  join refundrules in db.RefundsRules on refunds.RefundsId equals refundrules.RefundRuleId into val11
                                  from refundrules in val11.DefaultIfEmpty()
                                  join itineary in db.Itineraries on packagename.PackageID equals itineary.PackageID into val12
                                  from itineary in val12.DefaultIfEmpty()


                                  select new AllUserDetails
                                  {
                                      FirstName = user.FirstName,
                                      LastName = user.LastName,
                                      Email = user.Email,
                                      UserName = user.UserName,
                                      UserId = user.Id,
                                      PhoneNumber=user.PhoneNumber,
                                      Interest = interset.Interest,
                                      NumberOfTrips = user.NumberOfTrips,
                                      LoginProvider = login.LoginProvider,
                                      Role = role.Name,
                                      PackageBookingId = packagetemp.PackageBookingID,
                                      PaymentMethod = packagetemp.PaymentMethod,
                                      PackageId = packagetemp.PackageID,
                                      TotalPackageCost = packagetemp.TotalCost,
                                      PackageName = packagename.PackageName,
                                      StartDate = packagetemp.StartDate == null ? new DateTime() : packagetemp.StartDate,
                                      UserInterestID =(int?) interset.UserInterestID ?? 0,
                                      EndDate = packagetemp.EndDate == null ? new DateTime() : packagetemp.EndDate,
                                      PackageType = packagetype.Name,
                                      City = city.CityName,
                                      State = state.StateName
                                  }).Distinct().ToList();
            return alluserdetails;
        }
        List<UserProfileDTO> UserProfile = new List<UserProfileDTO>();




        public List<UserProfileJoinDTO> userprofile(string IUserName)
        {
            var val = (from user in db.Users
                       join interest in db.UserInterests on user.Id equals interest.Id into val1
                       from interest in val1.DefaultIfEmpty()
                       select new UserProfileJoinDTO
                       {
                           FirstName = user.FirstName,
                           LastName = user.LastName,
                           PhoneNumber = user.PhoneNumber,
                           UserName = user.UserName,
                           Interest = interest.Interest
                       })
                      .ToList();
            val = val.Where(x => x.UserName == IUserName).ToList();


            return val;
        }
        /// <summary>
        /// Put function to update the cancel status of booking
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="PackageBookingId"></param>
        /// <param name="PackageId"></param>
        public void CancelBooking(string Id, int PackageBookingId, int PackageId)
        {
            PackageBooking packageBooking = new PackageBooking();
            var associatedData = db.PackageBookings.Where(x => x.PackageBookingID == PackageBookingId).FirstOrDefault();
            packageBooking.Id = Id;
            packageBooking.PackageBookingID = PackageBookingId;
            packageBooking.PackageID = PackageId;
            packageBooking.BookingStatus = "Cancelled";
            packageBooking.StartDate = associatedData.StartDate;
            packageBooking.EndDate = associatedData.EndDate;
            packageBooking.PaymentMethod = associatedData.PaymentMethod;
            packageBooking.IsCustomized = associatedData.IsCustomized;
            packageBooking.TotalCost = associatedData.TotalCost;
            packageBooking.NumPeople = associatedData.NumPeople;
            //db.Entry(packageBooking).State = EntityState.Modified;
            db.Entry(associatedData).CurrentValues.SetValues(packageBooking);
            db.SaveChanges();

            Package package = new Package();
            if(packageBooking.IsCustomized)
            {
                package = (from custom in db.UserCustomizations
                           join pack in db.Packages on custom.PackageID equals pack.PackageID
                           where custom.CustomPackageID == PackageId
                           select pack).AsEnumerable().ToList()[0];
            }
            else
            {
                package = db.Packages.Where(x => x.PackageID == PackageId).FirstOrDefault();
            }
            //var packageassociatedData = db.Packages.Where(x => x.PackageID == PackageId).FirstOrDefault();
            //package.PackageID = PackageId;
            package.NumberAvailable = package.NumberAvailable++;
            /*package.PackageTypeID = packageassociatedData.PackageTypeID;
            package.PackageName = packageassociatedData.PackageName;
            package.Days = packageassociatedData.Days;
            package.ProfitPercentage = packageassociatedData.ProfitPercentage;
            package.Price = packageassociatedData.Price;
            package.Summary = packageassociatedData.Summary;
            package.MinPeople = packageassociatedData.MinPeople;
            package.MaxPeople= packageassociatedData.MaxPeople;
            package.Image = packageassociatedData.Image;*/

            db.Entry(package).CurrentValues.SetValues(package);
            db.SaveChanges();

            

            if (associatedData.PaymentMethod != "PayLater")
            {
                Refunds refunds = new Refunds();
                refunds.UserId = Id;
                refunds.RefundDate = DateTime.Now;
                refunds.PackageBookingID = PackageBookingId;
                int datediff = (int)(associatedData.StartDate - DateTime.Now).TotalDays;
                var value = db.RefundsRules.OrderByDescending(RefundsRules => RefundsRules.Days).Where(x => x.Days == datediff).FirstOrDefault();
                decimal val;
                if (value == null)
                {
                    refunds.RefundRuleId = 12;
                    refunds.RefundAmount = associatedData.TotalCost;
                }
                else
                {
                    val = value.RefundPercentage;
                    refunds.RefundRuleId = value.RefundRuleId;
                    decimal refundAmount = associatedData.TotalCost * (val / 100);
                    refunds.RefundAmount = associatedData.TotalCost - refundAmount;
                }
                
                
                //refunds.RefundAmount = associatedData.TotalCost * (val / 100);
                db.Refunds.Add(refunds);
                db.SaveChanges();
            }
        }

        public List<UserInterest> GetInterestByID(string id)
        {
            var interestToRetrieve = db.UserInterests.Where(x => x.Id == id).ToList();
            return interestToRetrieve;
        }
        /// <summary>
        /// Function to add interest in view profile
        /// </summary>
        /// <param name="Interest"></param>
        public void AddInterest(UserInterest Interest)
        {
            var interestDetails = (from x in db.UserInterests
                                  where x.Id == Interest.Id && x.Interest == Interest.Interest
                                  select x).ToList();

            try
            {  
                if (interestDetails.Count == 0)
                {
                    db.UserInterests.Add(Interest);
                    db.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception();
            }
        }
        /// <summary>
        /// Join query to display booking list of the user
        /// </summary>
        /// <returns></returns>
        public List<AllBookingUserList> AllBookingUserList()
        {
            /*var alluserdetails = (from user in db.Users
                                  join PackageBookings in db.PackageBookings on user.Id equals PackageBookings.Id
                                  join packagename in db.Packages on PackageBookings.PackageID equals packagename.PackageID
                                  join packagetype in db.PackageTypes on packagename.PackageTypeID equals packagetype.PackageTypeID
                                  join refunds in db.Refunds on PackageBookings.PackageBookingID equals refunds.PackageBookingID into val1
                                  from refunds in val1.DefaultIfEmpty()
                                  join refundrules in db.RefundsRules on refunds.RefundRuleId equals refundrules.RefundRuleId into val2
                                  from refundrules in val2.DefaultIfEmpty()


                                  select new AllBookingUserList
                                  {
                                      UserName = user.UserName,
                                      UserId = user.Id,
                                      NumberOfTrips = user.NumberOfTrips,
                                      PackageBookingId = PackageBookings.PackageBookingID,
                                      PaymentMethod = PackageBookings.PaymentMethod,
                                      PackageId = PackageBookings.PackageID,
                                      TotalPackageCost = PackageBookings.TotalCost,
                                      PackageName = packagename.PackageName,
                                      StartDate = PackageBookings.StartDate,
                                      EndDate = PackageBookings.EndDate,
                                      PackageType = packagetype.Name,
                                      BookingStatus = PackageBookings.BookingStatus,
                                      RefundAmount = refunds.RefundAmount,
                                      RefundPercentage = refundrules.RefundPercentage,
                                      Summary = packagename.Summary,
                                      Days = packagename.Days,
                                      IsCustomized = PackageBookings.IsCustomized
                                      //ActivityDetails = itineary.ActivityDetails,
                                      //DayNumber = itineary.DayNumber
                                  }).ToList();*/

            var alluserdetails = db.Database.SqlQuery<AllBookingUserList>(AllUserDetailString).AsEnumerable().ToList();

            foreach (AllBookingUserList booking in alluserdetails)
            {
                if (booking.IsCustomized)
                {
                    var package = (from custom in db.UserCustomizations
                                       join pack in db.Packages on custom.PackageID equals pack.PackageID
                                       join type in db.PackageTypes on pack.PackageTypeID equals type.PackageTypeID
                                       where custom.CustomPackageID == booking.PackageId
                                       select new {
                                           pack.PackageName,
                                           type.Name,
                                           pack.Days,
                                           pack.Summary
                                       }).AsEnumerable().ToList();

                    booking.PackageName = package[0].PackageName;
                    booking.PackageType = package[0].Name;
                    booking.Summary = package[0].Summary;
                    booking.Days = package[0].Days;
                }
            }
            return alluserdetails;
            }
        /// <summary>
        /// Delete interest in view profile
        /// </summary>
        /// <param name="ID"></param>
        public void DeleteInterest( int ID)
        {
            UserInterest InterestToDelete = db.UserInterests.Find(ID);
           
            try
            {
                if(InterestToDelete != null)
                {
                    db.UserInterests.Remove(InterestToDelete);
                    db.SaveChanges();
                }
                
            }
            catch
            {
                throw new Exception();
            }
        }
    }
}

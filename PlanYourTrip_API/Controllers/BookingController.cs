using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PlanYourTripDataAccessLayer;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;

namespace PlanYourTrip_API.Controllers
{
    public class BookingController : ApiController
    {
        readonly BookingManager bookingManager = new BookingManager();
        readonly PackageManager packageManager = new PackageManager();

        // Book package without payment
        [HttpPost]
        [Route("api/packages/book/")]
        public void BookPackage([FromBody] Booking booking)
        {
            PlanYourTripBusinessEntity.Models.PackageBooking packageBooking = new PlanYourTripBusinessEntity.Models.PackageBooking();
            packageBooking.PackageID = booking.PackageID;

            // Use identity framework to find user ID from username in Booking model
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);
            packageBooking.Id = userManager.FindByName(booking.UserName).Id;

            packageBooking.NumPeople = booking.NumPeople;
            packageBooking.StartDate = booking.StartDate.Date;
            packageBooking.EndDate = booking.EndDate.Date;
            packageBooking.PaymentMethod = booking.PaymentMethod;
            packageBooking.IsCustomized = booking.IsCustomized;
            packageBooking.TotalCost = booking.TotalCost;
            packageBooking.BookingStatus = booking.BookingStatus;
            bookingManager.BookPackage(packageBooking);

            // Increment number of trips taken by the user
            var user = userManager.FindById(packageBooking.Id);
            user.NumberOfTrips++;
            userManager.Update(user);
            userStore.Context.SaveChanges();

            // Decrement number available for package
            packageManager.DecrementNumAvailable(packageBooking.PackageID, packageBooking.NumPeople);
        }

        // Book with payment
        [HttpPost]
        [Route("api/packages/bookWithPayment/")]
        public void BookWithPayment([FromBody] BookWithPayment bwp)
        {
            PlanYourTripBusinessEntity.Models.PackageBooking packageBooking = new PlanYourTripBusinessEntity.Models.PackageBooking();
            packageBooking.PackageID = bwp.booking.PackageID;
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            packageBooking.Id = userManager.FindByName(bwp.booking.UserName).Id;
            packageBooking.NumPeople = bwp.booking.NumPeople;
            packageBooking.StartDate = bwp.booking.StartDate.Date;
            packageBooking.EndDate = bwp.booking.EndDate.Date;
            packageBooking.PaymentMethod = bwp.booking.PaymentMethod;
            packageBooking.IsCustomized = bwp.booking.IsCustomized;
            packageBooking.TotalCost = bwp.booking.TotalCost;
            packageBooking.BookingStatus = bwp.booking.BookingStatus;
            Payment pay = new Payment();
            pay.CreditCardNumber = bwp.payment.CreditCardNumber;
            pay.NameOnCard = bwp.payment.NameOnCard;
            pay.Amount = bwp.payment.Amount;
            bookingManager.BookWithPayment(packageBooking, pay);

            var user = userManager.FindById(packageBooking.Id);
            user.NumberOfTrips = user.NumberOfTrips++;
            userManager.Update(user);
            userStore.Context.SaveChanges();
        }

        [HttpPost]
        [Route("api/packages/bookCustom")]
        public void BookCustom([FromBody] CustomPackageBooking packageBooking)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            CustomPackage customPackage = new CustomPackage();
            customPackage.Id = userManager.FindByName(packageBooking.Booking.UserName).Id;
            int packageID = packageManager.AddCustomPackage(customPackage, packageBooking.UserCustomizations);

            PackageBooking booking = new PackageBooking();
            booking.PackageID = packageID;
            booking.Id = customPackage.Id;
            booking.NumPeople = packageBooking.Booking.NumPeople;
            booking.StartDate = packageBooking.Booking.StartDate.Date;
            booking.EndDate = packageBooking.Booking.EndDate.Date;
            booking.PaymentMethod = packageBooking.Booking.PaymentMethod;
            booking.IsCustomized = packageBooking.Booking.IsCustomized;
            booking.TotalCost = packageBooking.Booking.TotalCost;
            booking.BookingStatus = packageBooking.Booking.BookingStatus;
            bookingManager.BookPackage(booking);

            // Increment number of trips taken by the user
            var user = userManager.FindById(booking.Id);
            user.NumberOfTrips++;
            userManager.Update(user);
            userStore.Context.SaveChanges();

            // Decrement number available for package
            packageManager.DecrementNumAvailable(packageBooking.Booking.PackageID, booking.NumPeople);
        }

        [HttpPost]
        [Route("api/packages/bookCustomPayment")]
        public void BookCustomPayment([FromBody] CustomPackageBookingPayment packageBooking)
        {
            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new UserManager<ApplicationUser>(userStore);

            CustomPackage customPackage = new CustomPackage();
            customPackage.Id = userManager.FindByName(packageBooking.Booking.UserName).Id;
            int packageID = packageManager.AddCustomPackage(customPackage, packageBooking.UserCustomizations);

            PackageBooking booking = new PackageBooking();
            booking.PackageID = packageID;
            booking.Id = customPackage.Id;
            booking.NumPeople = packageBooking.Booking.NumPeople;
            booking.StartDate = packageBooking.Booking.StartDate.Date;
            booking.EndDate = packageBooking.Booking.EndDate.Date;
            booking.PaymentMethod = packageBooking.Booking.PaymentMethod;
            booking.IsCustomized = packageBooking.Booking.IsCustomized;
            booking.TotalCost = packageBooking.Booking.TotalCost;
            booking.BookingStatus = packageBooking.Booking.BookingStatus;
            // removed some fucntion here
            Payment pay = new Payment();
            pay.CreditCardNumber = packageBooking.Payment.CreditCardNumber;
            pay.NameOnCard = packageBooking.Payment.NameOnCard;
            pay.Amount = packageBooking.Payment.Amount;
            bookingManager.BookWithPayment(booking, pay);

            // Increment number of trips taken by the user
            var user = userManager.FindById(booking.Id);
            user.NumberOfTrips++;
            userManager.Update(user);
            userStore.Context.SaveChanges();

            // Decrement number available for package
            packageManager.DecrementNumAvailable(packageBooking.Booking.PackageID, booking.NumPeople);
        }
    }
}

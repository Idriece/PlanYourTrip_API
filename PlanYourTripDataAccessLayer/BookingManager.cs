using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanYourTripDataAccessLayer.Context;
using System.Web.Http;
using PlanYourTripBusinessEntity.Models;

namespace PlanYourTripDataAccessLayer
{
    public class BookingManager
    {
        readonly PlanYourTripData db = new PlanYourTripData();

        // Store a particular booking in the database
        public void BookPackage(PackageBooking booking)
        {
            db.PackageBookings.Add(booking);
            db.SaveChanges();
            if (!booking.IsCustomized)
            {
                int packageBookingId = db.PackageBookings.Max(x => x.PackageBookingID);
                AddCheckInRecords(booking, packageBookingId);
            }
            else
            {
                if (booking.IsCustomized)
                {
                    int customPackageId = db.UserCustomizations.Max(X => X.CustomPackageID);
                    AddCheckInRecords(booking, customPackageId);
                }
            }
        }

        // Store a booking and payment in database
        public void BookWithPayment(PackageBooking booking, Payment payment)
        {
            PackageBooking newBooking = db.PackageBookings.Add(booking);
            payment.PackageBookingID = newBooking.PackageBookingID;
            db.Payments.Add(payment);
            db.SaveChanges();
            if (!booking.IsCustomized)
            {
                int packageBookingId = db.PackageBookings.Max(x => x.PackageBookingID);
                AddCheckInRecords(booking, packageBookingId);
            }
            else if (booking.IsCustomized)
            {
                int customPackageId = db.UserCustomizations.Max(X => X.CustomPackageID);
                AddCheckInRecords(booking, customPackageId);
            }
        }


        //this function adds the checkin records in the table which will be later used to update the checkin status of a user
        //so that we can know if the user has started availing a package or not.
        public void AddCheckInRecords(PackageBooking booking, int packageBookingId)
        {
            int packageId = booking.PackageID;
            if (!booking.IsCustomized)
            {
                //handles non customized packages
                var itineraries = (from itir in db.Itineraries where itir.PackageID == packageId select itir).ToList<Itinerary>();
                foreach (var itir in itineraries)
                {
                    int? hotelId = db.RoomPrices.Find(itir.RoomPriceID).HotelID;
                    int? transportationProviderId = db.TransportationPrices.Find(itir.TransportationPriceID).TransportationProviderID;

                    UserCheckIn userCheckIn = new UserCheckIn();
                    userCheckIn.HotelCheckINStatus = false;
                    userCheckIn.TransportationCheckINStatus = false;
                    userCheckIn.CheckInDate = booking.StartDate.AddDays((itir.DayNumber - 1));
                    userCheckIn.PackageBookingID = packageBookingId;
                    userCheckIn.HotelID = hotelId.GetValueOrDefault();
                    userCheckIn.TransportationProviderID = transportationProviderId.GetValueOrDefault();
                    db.UserCheckIns.Add(userCheckIn);
                    db.SaveChanges();

                }
            }
            else if (booking.IsCustomized)
            {
                //Handles customized packages
                var customItineries = (from customItir in db.UserCustomizations where customItir.CustomPackageID == packageId select customItir).ToList<UserCustomization>();
                foreach (var customItir in customItineries)
                {
                    int? hotelId = db.RoomPrices.Find(customItir.RoomPriceID).HotelID;
                    int? transportationProviderId = db.TransportationPrices.Find(customItir.TransportationPriceID).TransportationProviderID;
                    UserCheckIn userCheckIn = new UserCheckIn();
                    userCheckIn.HotelCheckINStatus = false;
                    userCheckIn.TransportationCheckINStatus = false;
                    userCheckIn.CheckInDate = booking.StartDate.AddDays((customItir.DayNumber) - 1);
                    userCheckIn.PackageBookingID = db.PackageBookings.Max(x => x.PackageBookingID);
                    userCheckIn.HotelID = hotelId.GetValueOrDefault();
                    userCheckIn.TransportationProviderID = transportationProviderId.GetValueOrDefault();
                    db.UserCheckIns.Add(userCheckIn);
                    db.SaveChanges();
                }

            }
        }
    }
}

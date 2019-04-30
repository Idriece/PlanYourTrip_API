using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    public class TransportDAL
    {
        PlanYourTripData db = new PlanYourTripData();
        //This method return all the details associated with any transportation provider. 
        //Using tuple because we want to return more than one value from a method
        public List<Tuple<int, int, string, string, int, int, decimal, Tuple<string, string, string>>> GetAllTProviderDetailsDAL(string userName)
        {
            string userId = db.Users.Where(x => x.UserName == userName).FirstOrDefault().Id;
            var allTransportationDetails = (from transport in db.TransportationProviders
                                            join user in db.Users.Where(x => x.Id == userId) on transport.Id equals user.Id
                                            join city in db.Cities on transport.CityID equals city.CityID
                                            join transportPrice in db.TransportationPrices on transport.TransportationProviderID equals transportPrice.TransportationProviderID
                                            join transportMode in db.TransportationModes on transportPrice.TransportationModeID equals transportMode.TransportationModeID
                                            select new
                                            {
                                                transport.TransportationProviderID,
                                                transport.CityID,
                                                transport.TransportationProviderName,
                                                transport.Id,
                                                transportPrice.TransportationPriceID,
                                                transportPrice.TransportationModeID,
                                                transportPrice.Price,
                                                user.Email,
                                                city.CityName,
                                                transportMode.Name
                                            })
                                         .AsEnumerable() //asEnumerable operator to move query processing to client side
                                         .Select(x => new Tuple<int, int, string, string, int, int, decimal, Tuple<string, string, string>>(x.TransportationProviderID, x.CityID, x.TransportationProviderName, x.Id, x.TransportationPriceID,
                               x.TransportationModeID, x.Price, Tuple.Create<string, string, string>(x.Email, x.CityName, x.Name)))
                               .ToList();
            return allTransportationDetails;
        }

        //This method returns all the details associated with any transportation provider for that particular day ie.today's checkin
        public List<Tuple<int, int, bool, int, int, int, string, Tuple<string, string, string, DateTime, bool>>> getTodaysCheckInDAL(int transportationproviderID)
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            var todayCheckIn = (from checkin in db.UserCheckIns.Where(x => x.TransportationProviderID == transportationproviderID && x.CheckInDate == today)
                                join booking in db.PackageBookings on checkin.PackageBookingID equals booking.PackageBookingID
                                join user in db.Users on booking.Id equals user.Id
                                select new
                                {
                                    checkin.CheckInID,
                                    checkin.PackageBookingID,
                                    checkin.TransportationCheckINStatus,
                                    checkin.HotelID,
                                    checkin.TransportationProviderID,
                                    booking.NumPeople,
                                    booking.PaymentMethod,
                                    user.FirstName,
                                    user.LastName,
                                    user.Email,
                                    checkin.CheckInDate,
                                    checkin.HotelCheckINStatus,
                                }).AsEnumerable().Select(x => new Tuple<int, int, bool, int, int, int, string, Tuple<string, string, string, DateTime, bool>>(x.CheckInID, x.PackageBookingID, x.TransportationCheckINStatus, x.HotelID, x.TransportationProviderID, x.NumPeople, x.PaymentMethod, Tuple.Create<string, string, string, DateTime, bool>(x.FirstName, x.LastName, x.Email, x.CheckInDate, x.HotelCheckINStatus))).ToList();
            return todayCheckIn;

        }

        //This method returns all the details associated with any transportation provider for upcoming days ie.upcoming checkins
        public List<Tuple<string, bool, int, string, string, string, string, Tuple<DateTime, string>>> getUpcomingCheckIn(int transportationproviderID)
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            var upcomingCheckIn = (from checkin in db.UserCheckIns.Where(x => x.TransportationProviderID == transportationproviderID && x.CheckInDate > today)
                                join hotel in db.Hotels on checkin.HotelID equals hotel.HotelID
                                join transport in db.TransportationProviders on checkin.TransportationProviderID equals transport.TransportationProviderID
                                join booking in db.PackageBookings on checkin.PackageBookingID equals booking.PackageBookingID
                                join user in db.Users on booking.Id equals user.Id
                                select new
                                {
                                    hotel.HotelName,
                                    checkin.TransportationCheckINStatus,
                                    booking.NumPeople,
                                    booking.PaymentMethod,
                                    user.FirstName,
                                    user.LastName,
                                    user.Email,
                                    checkin.CheckInDate,
                                    transport.TransportationProviderName
                                }).AsEnumerable().Select(x => new Tuple< string, bool, int, string, string, string, string, Tuple<DateTime, string>>(x.HotelName, x.TransportationCheckINStatus, x.NumPeople, x.PaymentMethod, x.FirstName, x.LastName, x.Email, Tuple.Create<DateTime, string>(x.CheckInDate, x.TransportationProviderName))).ToList();
            return upcomingCheckIn;
        }

        //This method returns all the details associated with any transportation provider for Past days ie.all the past checkins

        public List<Tuple<string, bool, int, string, string, string, string, Tuple<DateTime, string>>> getPastCheckInDAL(int transportationproviderID)
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            var pastCheckIns = (from checkin in db.UserCheckIns.Where(x => x.TransportationProviderID == transportationproviderID && x.CheckInDate < today)
                                join hotel in db.Hotels on checkin.HotelID equals hotel.HotelID
                                join transport in db.TransportationProviders on checkin.TransportationProviderID equals transport.TransportationProviderID
                                join booking in db.PackageBookings on checkin.PackageBookingID equals booking.PackageBookingID
                                join user in db.Users on booking.Id equals user.Id
                                select new
                                {
                                    hotel.HotelName,
                                    checkin.TransportationCheckINStatus,
                                    booking.NumPeople,
                                    booking.PaymentMethod,
                                    user.FirstName,
                                    user.LastName,
                                    user.Email,
                                    checkin.CheckInDate,
                                    transport.TransportationProviderName
                                }).AsEnumerable().Select(x => new Tuple<string, bool, int, string, string, string, string, Tuple<DateTime, string>>(x.HotelName, x.TransportationCheckINStatus, x.NumPeople, x.PaymentMethod, x.FirstName, x.LastName, x.Email, Tuple.Create<DateTime, string>(x.CheckInDate, x.TransportationProviderName))).ToList();
            return pastCheckIns;
        }

        // This method is used to update the checkin status of the user by the transportation provioder for the start date of the journey ie. todays checkin

        public void PutTransportCheckInStatusDAL(UserCheckIn userCheckIn)
        {
            db.Entry(userCheckIn).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
   
        }

        public bool CheckInExists(int id)
        {
            return db.UserCheckIns.Count(x => x.CheckInID == id) > 0;
        }
    }
}

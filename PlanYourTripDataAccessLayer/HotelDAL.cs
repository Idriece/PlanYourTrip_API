using Microsoft.AspNet.Identity;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripDataAccessLayer
{
    public class HotelDAL
    {
        PlanYourTripData db = new PlanYourTripData();
        public List<Tuple<int, int, string, string, string, int, int, Tuple<decimal, string, string, string>>> getAllHoteldetailDAL(string userName)
        {
            string userId = db.Users.Where(x => x.UserName == userName).Select(x => x.Id).FirstOrDefault();

            var allHotelDetails = (from hotel in db.Hotels
                                   join user in db.Users.Where(x => x.Id == userId) on hotel.Id equals user.Id
                                   join city in db.Cities on hotel.CityID equals city.CityID
                                   join roomPrice in db.RoomPrices on hotel.HotelID equals roomPrice.HotelID
                                   join roomType in db.RoomTypes on roomPrice.RoomTypeID equals roomType.RoomTypeID
                                   select new
                                   {
                                       hotel.HotelID,
                                       hotel.CityID,
                                       hotel.HotelName,
                                       hotel.Id,
                                       hotel.HotelImage,
                                       roomPrice.RoomPriceID,
                                       roomPrice.RoomTypeID,
                                       roomPrice.Price,
                                       user.Email,
                                       city.CityName,
                                       roomType.Type

                                   }).AsEnumerable()
                                     .Select(x => new Tuple<int, int, string, string, string, int, int, Tuple<decimal, string, string, string>>(x.HotelID, x.CityID, x.HotelName, x.Id, x.HotelImage, x.RoomPriceID, x.RoomTypeID, Tuple.Create<decimal, string, string, string>(x.Price, x.Email, x.CityName, x.Type)))
                                                 .ToList();
            return allHotelDetails;
        }

        public List<Tuple<int, int, bool, int, int, int, string, Tuple<string, string, string, DateTime, bool>>> getTodaysCheckInDAL(int? hotelID)
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            var todayCheckIn = (from checkin in db.UserCheckIns.Where(x => x.HotelID == hotelID && x.CheckInDate == today)
                                join booking in db.PackageBookings on checkin.PackageBookingID equals booking.PackageBookingID
                                join user in db.Users on booking.Id equals user.Id
                                select new
                                {
                                    checkin.CheckInID,
                                    checkin.PackageBookingID,
                                    checkin.HotelCheckINStatus,
                                    checkin.HotelID,
                                    checkin.TransportationProviderID,
                                    booking.NumPeople,
                                    booking.PaymentMethod,
                                    user.FirstName,
                                    user.LastName,
                                    user.Email,
                                    checkin.CheckInDate,
                                    checkin.TransportationCheckINStatus
                                }).AsEnumerable().Select(x => new Tuple<int, int, bool, int, int, int, string, Tuple<string, string, string, DateTime, bool>>(x.CheckInID, x.PackageBookingID, x.HotelCheckINStatus, x.HotelID, x.TransportationProviderID, x.NumPeople, x.PaymentMethod, Tuple.Create<string, string, string, DateTime, bool>(x.FirstName, x.LastName, x.Email, x.CheckInDate, x.TransportationCheckINStatus))).ToList();
            return todayCheckIn;
        }

        public List<Tuple<string, bool, int, string, string, string, string, Tuple<DateTime, string, string>>> getPastCheckInDAL(int hotelID)
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            var pastCheckIn = (from checkin in db.UserCheckIns.Where(x => x.HotelID == hotelID && x.CheckInDate < today)
                               join hotel in db.Hotels on checkin.HotelID equals hotel.HotelID
                               join transport in db.TransportationProviders on checkin.TransportationProviderID equals transport.TransportationProviderID
                               join booking in db.PackageBookings on checkin.PackageBookingID equals booking.PackageBookingID
                               join user in db.Users on booking.Id equals user.Id
                               select new
                               {
                                   hotel.HotelName,
                                   checkin.HotelCheckINStatus,
                                   booking.NumPeople,
                                   booking.PaymentMethod,
                                   user.FirstName,
                                   user.LastName,
                                   user.Email,
                                   checkin.CheckInDate,
                                   transport.TransportationProviderName,
                                   user.PhoneNumber
                               }).AsEnumerable().Select(x => new Tuple<string, bool, int, string, string, string, string, Tuple<DateTime, string, string>>(x.HotelName, x.HotelCheckINStatus, x.NumPeople, x.PaymentMethod, x.FirstName, x.LastName, x.Email, Tuple.Create<DateTime, string, string>(x.CheckInDate, x.TransportationProviderName, x.PhoneNumber))).ToList();
            return pastCheckIn;
        }
        public List<Tuple<string, bool, int, string, string, string, string, Tuple<DateTime, string, string>>> getUpcomingCheckIn(int hotelID)
        {
            DateTime today = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0, 0);
            var upcomingCheckIn = (from checkin in db.UserCheckIns.Where(x => x.HotelID == hotelID && x.CheckInDate > today)
                                join hotel in db.Hotels on checkin.HotelID equals hotel.HotelID
                                join transport in db.TransportationProviders on checkin.TransportationProviderID equals transport.TransportationProviderID
                                join booking in db.PackageBookings on checkin.PackageBookingID equals booking.PackageBookingID
                                join user in db.Users on booking.Id equals user.Id
                                select new
                                {
                                    hotel.HotelName,
                                    checkin.HotelCheckINStatus,
                                    booking.NumPeople,
                                    booking.PaymentMethod,
                                    user.FirstName,
                                    user.LastName,
                                    user.Email,
                                    checkin.CheckInDate,
                                    transport.TransportationProviderName,
                                    user.PhoneNumber
                                }).AsEnumerable().Select(x => new Tuple< string, bool, int, string, string, string, string, Tuple<DateTime, string, string>>(x.HotelName, x.HotelCheckINStatus, x.NumPeople, x.PaymentMethod, x.FirstName, x.LastName, x.Email, Tuple.Create<DateTime, string, string>(x.CheckInDate, x.TransportationProviderName, x.PhoneNumber))).ToList();
            return upcomingCheckIn;
        }

        public bool PutHotelCheckInStatusDAL(UserCheckIn userCheckIn)
        {
            db.Entry(userCheckIn).State = System.Data.Entity.EntityState.Modified;
            try
            {
                db.SaveChanges();
                
            }
            catch (Exception)
            {
                if(!CheckInExists(userCheckIn.CheckInID)) {
                    return false;
                }
                else
                {
                    throw new Exception();
                }
            }
            return true;
        }

        public bool CheckInExists(int id)
        {
            return db.UserCheckIns.Count(x => x.CheckInID == id) > 0;
        }

    }
}

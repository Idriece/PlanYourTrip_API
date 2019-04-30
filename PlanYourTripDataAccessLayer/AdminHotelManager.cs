using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace PlanYourTripDataAccessLayer
{
    public class AdminHotelManager
    {
        readonly PlanYourTripData db = new PlanYourTripData();
        public bool NewHotelRoomPriceAdder(RoomPrice roomPrice)
        {
            db.RoomPrices.Add(roomPrice);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
                throw new Exception();
            }
            return true;
        }
        public int AddHotelToDb(Hotel hotel)
        {
                db.Hotels.Add(hotel);
                db.SaveChanges();
                int newHotelId = db.Hotels.Max(p => p.HotelID);
                return newHotelId;
        }

        public void AddHotelPrice(RoomPrice price)
        {
            try {
                db.RoomPrices.Add(price);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw new DbUpdateConcurrencyException();
            }

        }

        public List<City> getAllCities(){

            return db.Cities.ToList();
            }


        
        public List<Tuple<string, string>> GetOwnerList()
        {
            var userEmailDTOList = 
                (from user in db.Users
                 join userRole in db.UserRole.Where(x => x.RoleId == 3.ToString()) on user.Id equals userRole.UserId
                 select new {
                            user.Id,
                            user.Email
                            })  
                            .AsEnumerable()
                            .Select(x => new Tuple<string, string>(x.Id, x.Email)).ToList();
            return userEmailDTOList;
        }


        public List<Tuple<int, int, string, string, string, int, int, Tuple<decimal, string, string, string>>> GetAllHotelsDetails()
        {   
            var allHotelDetails = (from hotel in db.Hotels
                                  join user in db.Users on hotel.Id equals user.Id
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
        public bool updateHotelDetail(Hotel hotel)
        {
            db.Entry(hotel).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                if (!HotelExist(hotel.HotelID))
                {
                    return false;
                }
                throw new Exception();
                
            }

        }

        private bool HotelExist(int id)
        {
            return db.Hotels.Count(e => e.HotelID == id) > 0;
        }

        public void updateHotelPrice(RoomPrice price)
        {
            db.Entry(price).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
               
            }
            catch (DbUpdateConcurrencyException)
            {
                
                throw new Exception();
            }
        }
        
        /// <summary>
        /// Search Booking by Date
        /// </summary>
        /// <param Start Date="Start"></param>
        /// <param End Date="End"></param>
        /// <returns></returns>
        public List<AllBookingUserList> PackageBooking(DateTime Start, DateTime End)
        {
            AdminUserManager adminUser = new AdminUserManager();
            //var BookingList = db.PackageBookings.Where(x => EntityFunctions.TruncateTime(x.StartDate) == Start.Date && EntityFunctions.TruncateTime(x.EndDate) == End.Date).ToList();

            var BookingList = adminUser.AllBookingUserList().Where(x => (x.StartDate.Value).Date >= Start.Date && (x.EndDate.Value).Date <= End.Date).ToList();


            //var BookingList = adminUser.AllUserDetails().Where(x =>true &&
            //EntityFunctions.TruncateTime(x.StartDate) == Start.Date && EntityFunctions.TruncateTime(x.EndDate) == End.Date).ToList();

            return BookingList;
        }


        public List<Tuple<string, string>> GetAllHotelCityListDAL()
        {
            var allHotelCity = (from hotel in db.Hotels
                                join cities in db.Cities on hotel.CityID equals cities.CityID
                                select new
                                {
                                    hotel.HotelName,
                                    cities.CityName
                                }).AsEnumerable().Select(x => new Tuple<string, string>(x.HotelName, x.CityName)).ToList();
            return allHotelCity;
        }
    }
}

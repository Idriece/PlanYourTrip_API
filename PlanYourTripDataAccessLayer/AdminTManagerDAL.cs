using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;

namespace PlanYourTripDataAccessLayer
{
    public class AdminTManagerDAL
    {
        readonly PlanYourTripData db = new PlanYourTripData();
        public int AddTProviderDAL(TransportationProvider transportationProvider)
        {
            db.TransportationProviders.Add(transportationProvider);
            db.SaveChanges();

            int newTProviderID = db.TransportationProviders.Max(p => p.TransportationProviderID);
            return newTProviderID;
        }

        public void AddTProviderPriceDAL(TransportationPrice transportationPrice)
        {
            db.TransportationPrices.Add(transportationPrice);
            db.SaveChanges();
        }

        public List<Tuple<string, string>> GetOwnerList()
        {
            var userEmailDTOList =
                (from user in db.Users
                 join userRole in db.UserRole.Where(x => x.RoleId == 4.ToString()) on user.Id equals userRole.UserId
                 select new
                 {
                  user.Id,
                  user.Email
                 }).AsEnumerable()
                   .Select(x => new Tuple<string, string>(x.Id, x.Email)).ToList();
            return userEmailDTOList;
        }


        public List<Tuple<string, string>>   GetAllTransportCityListDAL()
        {
            var allTransportHotel = (from transport in db.TransportationProviders
                                     join cities in db.Cities on transport.CityID equals cities.CityID
                                     select new
                                     {
                                         transport.TransportationProviderName,
                                         cities.CityName
                                     }
                                    ).AsEnumerable().Select(x => new Tuple<string, string>(x.TransportationProviderName, x.CityName)).ToList();
            return allTransportHotel;
        }

    //This method return all the details associated with any transportation provider. 
        public List<Tuple<int, int, string, string, int, int, decimal, Tuple< string, string, string>>> GetAllTProviderDetailsDAL()
        {
            var allTransportationDetails = (from transport in db.TransportationProviders
                                            join user in db.Users on transport.Id equals user.Id
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
                                         .AsEnumerable()
                                         .Select(x => new Tuple<int, int, string, string, int, int, decimal, Tuple<string, string, string>>(x.TransportationProviderID, x.CityID, x.TransportationProviderName, x.Id, x.TransportationPriceID,
                               x.TransportationModeID, x.Price, Tuple.Create<string, string, string>(x.Email, x.CityName, x.Name)))
                               .ToList();
            return allTransportationDetails;
        }

        public void UpdateTProviderDAL(TransportationProvider transportationProvider)
        {

            db.Entry(transportationProvider).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!TProviderExists(transportationProvider.TransportationProviderID))
                {
                    throw;
                }
                throw;

            }
        }
        public void UpdateTProviderPriceDAL(TransportationPrice transportationPrice)
        {
            db.Entry(transportationPrice).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                if (!TProviderPriceExists(transportationPrice.TransportationPriceID))
                {
                   
                }
            
            }
        }

        public bool AddNewPriceDAL(TransportationPrice transportationPrice)
        {
            db.TransportationPrices.Add(transportationPrice);
            try
            {
                db.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
        }

        private bool TProviderExists(int id)
        {
            return db.TransportationProviders.Count(e => e.TransportationProviderID == id) > 0;
        }
        private bool TProviderPriceExists(int id)
        {
            return db.TransportationPrices.Count(e => e.TransportationPriceID == id) > 0;
        }
    }
}

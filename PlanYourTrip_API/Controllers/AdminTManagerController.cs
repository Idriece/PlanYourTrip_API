using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripBusinessLogic;

namespace PlanYourTrip_API.Controllers
{
    public class AdminTManagerController : ApiController
    {
        readonly AdminTManagerBL adminTManagerBL = new AdminTManagerBL();

        //creates transportation providers and and recives an array of trandportation mode along with
        //their prices and all this information is saved in db.
        [HttpPost]
        [Route("api/AdminTManager/createTProvider")]
        public IHttpActionResult CreateTProviderC(AddTProviderDTO addTProviderDTO)
        {
            if(addTProviderDTO == null)
            {
                return BadRequest("Please Provide Valid Transportation Providers Details ");
            }
            if (!ModelState.IsValid)
            {
                string modelErrors = string.Join(Environment.NewLine, ModelState.Values
                                                                      .SelectMany(x => x.Errors)
                                                                      .Select(x => x.ErrorMessage));
                return BadRequest(modelErrors);
            }
            try
            {
                TransportationProvider transportationProvider = new TransportationProvider();
                transportationProvider.CityID = addTProviderDTO.CityId;
                transportationProvider.TransportationProviderName = addTProviderDTO.TransportationProviderName;
                transportationProvider.Id = addTProviderDTO.UserId;

                var TProviderID = adminTManagerBL.AddTProviderBL(transportationProvider);

                List<TransportationPrice> transportationPrices = new List<TransportationPrice>();

                foreach (var price in addTProviderDTO.TransportationPrices)
                {
                    transportationPrices.Add(
                        new TransportationPrice()
                        {
                            TransportationProviderID = TProviderID,
                            TransportationModeID = price.TransportationModeID,
                            Price = price.Price
                        });
                }

                adminTManagerBL.AddTProviderPriceBL(transportationPrices);
                return Ok();
            }
            catch(Exception ex)
            {
                if (ex.GetType().IsAssignableFrom(typeof(DbUpdateConcurrencyException)))
                {
                    ModelState.AddModelError("DuplicateTransportPriceAdditionError", "Combination of price and hotel already exist");
                    return BadRequest(ModelState);
                }
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while creating a new transportation provider"));
            }
        }

        //it returns all the details associated with a transport provider
        [HttpGet]
        [Route("api/AdminTManager/GetAllTProviderDetails")]
        public IHttpActionResult GetAllTProviderDetails()
        {
            try
            {
                var allTProviderDetails = adminTManagerBL.GetAllTProviderDetailsBL();
                return Ok(allTProviderDetails);
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occoured While fetching all transportation Porviders details"));
            }
        }

        //gets all cities from db
        [HttpGet]
        [Route("api/AdminTManager/GetAllTransportCityList")]
        public IHttpActionResult GetAllTransportCityListCL()
        {
            try
            {
                var allTransportCity = adminTManagerBL.GetAllTransportCityListBL();
                return Ok(allTransportCity);
            }
            catch(Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while fetching the transportation provider name and their city, Error: " + e.Message));
            }
        }
        //updates the details of a transportation providers.
        [HttpPut]
        [Route("api/AdminTManager/UpdateTProvider")]
        public IHttpActionResult UpdateTProvider(UpdateTProviderDTO updateTProviderDTO)
        {
            if (updateTProviderDTO == null)
            {
                return BadRequest("Please provide valid details for updating the transportation provider");
            }
            if (!ModelState.IsValid)
            {
                string modelErrors = string.Join(Environment.NewLine, ModelState.Values
                                                                      .SelectMany(x => x.Errors)
                                                                      .Select(x => x.ErrorMessage));
                return BadRequest(modelErrors);
            }
            try
            {
                TransportationProvider transportationProvider = new TransportationProvider();
                transportationProvider.TransportationProviderID = updateTProviderDTO.TransportationProviderID;
                transportationProvider.CityID = updateTProviderDTO.CityID;
                transportationProvider.Id = updateTProviderDTO.Id;
                transportationProvider.TransportationProviderName = updateTProviderDTO.TransportationProviderName;

                adminTManagerBL.UpdateTProviderBL(transportationProvider);

                List<TransportationPrice> updatedTransportationPrices = new List<TransportationPrice>();

                foreach (var updatedPrice in updateTProviderDTO.updatedTProviderPrice)
                {
                    updatedTransportationPrices.Add(new TransportationPrice()
                    {
                        TransportationProviderID = updateTProviderDTO.TransportationProviderID,
                        TransportationPriceID = updatedPrice.TransportationPriceID,
                        TransportationModeID = updatedPrice.TransportationModeID,
                        Price = updatedPrice.Price
                    });
                }

                adminTManagerBL.UpdateTProviderPrices(updatedTransportationPrices);
                return Ok();
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while updating the transportation provider"));
            }
        }

        //while updating any transportationprovider details  if any price doesn't exist previously 
        //then in that scenario this method handles addition of new prices.
        [HttpPost]
        [Route("api/AdminTManager/AddNewPrice")]
        public IHttpActionResult AddNewPrice(AddNewPrice AddTPrice)
        {
            if(AddTPrice == null)
            {
                return BadRequest("Please Provide Valid New Price in correct format.");
            }
            if (!ModelState.IsValid)
            {
                string modelErrors = string.Join(Environment.NewLine, ModelState.Values
                                                                      .SelectMany(x => x.Errors)
                                                                      .Select(x => x.ErrorMessage));
                return BadRequest(modelErrors);
            }
            try
            {
                TransportationPrice transportationPrice = new TransportationPrice();
                transportationPrice.TransportationProviderID = AddTPrice.TransportationProviderID;
                transportationPrice.TransportationModeID = AddTPrice.TransportationModeID;
                transportationPrice.Price = AddTPrice.Price;

                bool response = adminTManagerBL.AddNewPrice(transportationPrice);
                if (!response)
                {
                    return NotFound();
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NoContent, "Successfully posted New Price"));
                }
            }
            catch(Exception ex)
            {
                if(ex.GetType().IsAssignableFrom(typeof(DbUpdateConcurrencyException)))
                {
                    ModelState.AddModelError("DuplicateTransportationPriceAdditionError", "Combination of price and Transport already exist");
                    return BadRequest(ModelState);
                }
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while trying to add new transportation price"));
            }
        }

        //this gets the email id's of those owners who are registered as transportation providers.
        [HttpGet]
        [Route("api/AdminTManager/transowner")]
        public IHttpActionResult TransportationOwner()
        {
            try
            {
                var ownerList = adminTManagerBL.GetOwnerList();
                return Ok(ownerList);
            }
            catch(Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occured while fetching the transportation owner email id list"));
            }
        }
    }
}

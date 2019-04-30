using Newtonsoft.Json.Linq;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripBusinessLogic;
using PlanYourTripException;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;

namespace PlanYourTrip_API.Controllers
{
    public class HotelController : ApiController
    {
        readonly HotelBuisnessLogic hotelBuisnessLogic = new HotelBuisnessLogic();

        //gets all hotels details asociated with the owner who corresponds to the id which is passed
        [HttpGet]
        [Route("api/Hotel/{id}")]
        public IHttpActionResult GetAllHotelDetails(string id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest("Plese provide a not null id");
                }
                var allHotelDetails = hotelBuisnessLogic.getAllHotelDetails(id);
                return Ok(allHotelDetails);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occoured While getting details"));
            }
        }

        //methodto get today's checkin for a particular hotel
        [HttpGet]
        [Route("api/CheckinsT/{id}")]
        public IHttpActionResult GetTodaysCheckIn(int? id)
        {
            try
            {
                if (id == null)
                    return BadRequest("ID cannot be null!");

                var todaysCheckIn = hotelBuisnessLogic.getTodaysCheckIn(id);
                return Ok(todaysCheckIn);
            }
            catch (Exception ex)
            {
                //TODO: use log4net package to log exceptions
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error occoured while fetching today's checkin's "));
            }
        }

        //method to get past checkins for a particular hotel
        [HttpGet]
        [Route("api/CheckinsP/{id}")]
        public IHttpActionResult GetPastCheckIn(int? id)
        {
            if (id == null)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NotFound, "Enter a valid Id."));
            }
            try
            {
                var pastCHeckIn = hotelBuisnessLogic.getPastCheckIn(id.GetValueOrDefault());
                return Ok(pastCHeckIn);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("Some Error Occoured while fetching past booking for the hotel with ID {0}", id)));
            }
        }

        //methodto get future checkins for a particular hotel whose is is equals to the passed id 
        [HttpGet]
        [Route("api/CheckinsF/{id}")]
        public IHttpActionResult GetUpcomingCheckIn(int id)
        {
            try
            {
                var upcomingCheckIn = hotelBuisnessLogic.getUpcomingCheckIn(id);
                return Ok(upcomingCheckIn);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, string.Format("Some Error Occoured while fetching Future booking for the hotel with ID {0}", id)));
            }
        }

        //method to update hotel chekcin statusfor a particular booking where booking date is today
        [HttpPut]
        [Route("api/hotelcheckin")]
        public IHttpActionResult PutHotelCheckInStatus(UserCheckIn userCheckIn)
        {
            //Guard clauses

            //Negative check 1
            if (userCheckIn == null)
            {
                return BadRequest("Please send valid data for updating the user checkin status");
            }

            //Negative check 2
            if (!ModelState.IsValid)
            {
                string modelErrors = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));

                return BadRequest(modelErrors);
            }

            try
            {
                bool response = hotelBuisnessLogic.PutHotelCheckInStatusBL(userCheckIn);
                if (!response)
                {
                    return NotFound();
                }
                else
                {
                    return StatusCode(HttpStatusCode.NoContent);
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occoured While Updating CheckIn Status"));
            }
        }
    }
}

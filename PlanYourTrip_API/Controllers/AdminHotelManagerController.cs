using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripDataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using PlanYourTripBusinessLogic;
using Newtonsoft.Json.Linq;
using System.Data.Entity.Infrastructure;
using Newtonsoft.Json;
using System.Web;
using System.IO;
using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace PlanYourTrip_API.Controllers
{
    public class AdminHotelManagerController : ApiController
    {
        readonly HotelManagerBuisinessLogic hotelManager = new HotelManagerBuisinessLogic();

        //This method is called when admin tries to edit any existing hotel and he adds a new price which was
        //not added while he was creating the hotel so we handle such situations using this post method.
        [HttpPost]
        [Route("api/addNewPrice")]
        public IHttpActionResult PostNewHotelPrice(RoomPrice roomPrice)
        {
            if(roomPrice == null)
            {
                return BadRequest("Please provide valid room prices");
            }
            if(!ModelState.IsValid)
            {
                string modelErrors = string.Join(Environment.NewLine, ModelState.Values
                                        .SelectMany(x => x.Errors)
                                        .Select(x => x.ErrorMessage));
                return BadRequest(modelErrors);
            }
            try {
            bool response = hotelManager.AddNewHotelRoomPrice(roomPrice);
                if (!response) {
                    return ResponseMessage(Request.CreateErrorResponse((HttpStatusCode)422, new HttpError("Unprossesed Entity! Something went wrong while adding room prices")));
                }
                else
                {
                    return Ok();
                }

            }
            catch(Exception e)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some Error Occoured While adding the room price value"));
            }
        }

        //This mehtod is called when admin attempts to add new hotel.
        //it recieves hotel details and a list of prices for different types of room
        //this mehtod first create a hotel in Hotel table and get the primary key of hotel table and 
        //use it to create entries of RoomPrice table
        [HttpPost]
        [Route("api/AdminHotelManager")]
        public IHttpActionResult PostHotelDetail()
        {
            try
            {
                HotelDetail hotelDto;;
                hotelDto = JsonConvert.DeserializeObject<HotelDetail>(HttpContext.Current.Request.Form["hotelData"]);
                string imageName = "";
                var postedFile = HttpContext.Current.Request.Files["ImageFile"];
                imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ","-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);

                string connectionString = ConfigurationManager.ConnectionStrings["ImageBlobStorage"].ToString();

                CloudStorageAccount imageStorage = CloudStorageAccount.Parse(connectionString);
                CloudBlobClient blobClient = imageStorage.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("planyourtrippackagecontainer");
                container.CreateIfNotExists();
                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
                CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(imageName);
                cloudBlockBlob.UploadFromStream(postedFile.InputStream);
                Hotel hotel = new Hotel();
                hotel.CityID = hotelDto.CityId;
                hotel.HotelImage = cloudBlockBlob.Uri.ToString();
                hotel.HotelName = hotelDto.HotelName;
                hotel.Id = hotelDto.UserId;

                //id of the newly inserted hotel is stored in hotelId
                var hotelId = hotelManager.AddHotel(hotel);

                //contains value for prices for different room
                List<RoomPrice> roomPrices = new List<RoomPrice>();

                foreach (var price in hotelDto.RoomPrices)
                {
                    roomPrices.Add(
                        new RoomPrice()
                        {
                            HotelID = hotelId,
                            Price = price.Price,
                            RoomTypeID = price.RoomTypeID,
                        });
                }

                //a list of prices is passed to this method which is defined in business layer
                hotelManager.AddHotelPrice(roomPrices);
                return Ok();
            } catch(Exception ex)
            {
                if(ex.GetType().IsAssignableFrom(typeof(DbUpdateConcurrencyException)))
                {
                    ModelState.AddModelError("DuplicateHotelPriceAdditionError", "Combination of price and hotel already exist");
                    return BadRequest(ModelState);
                }
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while trying to add new hotel"));
            }
        }

       //this mehtod returns  us the list of cityName and CityID from city table, it gets it from method getAllCities
       //which is defined in business layer.
       [HttpGet]
       [Route("api/Cities")]
        public IHttpActionResult GetCities()
        {
            try
            {
                var allCities = hotelManager.getAllCities();
                return Ok(allCities);
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while getting the cities list"));
            }
        }


        //this methos returns us the list of UserID and email from users table , it gets it from method GetOwnerList,
        //which is defined in business layer
        [HttpGet]
        [Route("api/OwnerList")]
        public IHttpActionResult GetOwnerList()
        {
            try {
                var getOwnerList = hotelManager.GetOwnerList();
                return Ok(getOwnerList);

            } catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while getting the Owner Email Id list"));
            }
}
        //this method gets us all details associated with any hotel present in our db, this data is used by to view hotels 
        //details in admin's hotel view
        [HttpGet]
        [Route("api/getAllHotelDetails")]
        public IHttpActionResult hotelsdetails()
        {
            try
            {
                var allHotelsDetails = hotelManager.getAllHotelDetails();
                return Ok(allHotelsDetails);
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while trying to fetch all the hotel details"));
            }
        }


        //this method handles when admin updates records for existing hotel
        [HttpPut]
        [Route("api/AdminHotelManager/{id}")]
        public IHttpActionResult PutHotel(int id, UpdatedHotelDetailDTO updatedHotelDetailDTO)
        {
            if(updatedHotelDetailDTO == null)
            {
                return BadRequest("Please Provide us valid details for update purpose");
            }
            if(!ModelState.IsValid)
            {
                string modelErrors = string.Join(Environment.NewLine, ModelState.Values
                                                                      .SelectMany(x => x.Errors)
                                                                      .Select(x => x.ErrorMessage));
                return BadRequest(modelErrors);
            }
            try
            {
                Hotel hotel = new Hotel();
                hotel.HotelID = updatedHotelDetailDTO.HotelID;
                hotel.CityID = updatedHotelDetailDTO.CityID;
                hotel.HotelName = updatedHotelDetailDTO.HotelName;
                hotel.Id = updatedHotelDetailDTO.Id;
                hotel.HotelImage = updatedHotelDetailDTO.HotelImage;

                bool hotelUpdateStatus = hotelManager.PutHotelDetail(hotel);
                List<RoomPrice> updatedRoomPrices = new List<RoomPrice>();
                foreach (var updatedPrice in updatedHotelDetailDTO.updatedHotelPriceDTO)
                {
                    updatedRoomPrices.Add(
                        new RoomPrice()
                        {
                            HotelID = updatedHotelDetailDTO.HotelID,
                            RoomPriceID = updatedPrice.RoomPriceID,
                            RoomTypeID = updatedPrice.RoomTypeID,
                            Price = updatedPrice.Price

                        });
                }
                hotelManager.PutHotelPrice(updatedRoomPrices);
                if (!hotelUpdateStatus)
                {
                    return NotFound();
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NoContent, "Successfully updated the record"));
                }
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while updating the hotel details"));
            }
        }

        /// <summary>
        /// Search Booking by Date
        /// </summary>
        /// <param Start Date="Start"></param>
        /// <param End Date="End"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("api/booking/bookinglist")]
        public List<AllBookingUserList> CPackageBooking(DateTime Start, DateTime End)
        {
            return hotelManager.LPackageBooking(Start, End);
        }

        

        [HttpGet]
        [Route("api/AdminHotelManager/GetHotelCity")]
        public IHttpActionResult GetHotelCity()
        {
            try
            {
                var hotelCityList = hotelManager.GetAllHotelCityListBL();
                return Ok(hotelCityList);
            }
            catch(Exception)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while fetching the hotel, city pairs"));
            }
        }
    }
}

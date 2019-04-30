using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using PlanYourTrip_API.Models;
using PlanYourTripBusinessEntity.Models;
using PlanYourTripBusinessLogic;


namespace PlanYourTrip_API.Controllers
{
    [RoutePrefix("api/packages")]
    public class PackagesController : ApiController
    {
        PackageBLL pl = new PackageBLL();
        //get method to list package type
        [System.Web.Http.HttpGet]
        public IHttpActionResult PackageTypes()
        {
            try
            {
                var packType = pl.PackageTypes();
                return Ok(packType);
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occoured while getting the Package Types"));
            }
        }

        //get method to list all package

        [HttpGet]
        public  IHttpActionResult GetPackages()
        {
            try
            {
                var pack = pl.GetPackage();
                return Ok(pack);
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occoured while getting Package"));
            }
            
        }

        //get method to get roomprice of all hotels

        [HttpGet]
        [ResponseType(typeof(RoomPriceForAdmin))]
        public IHttpActionResult GetRoomPriceForAdmin()
        {
            try
            {
                var roomPrice = pl.FetchRoomPrice();
                return Ok(roomPrice);
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occoured while getting Room Price"));
            }
        }

        //get method to get transportation price of all Travels

        [HttpGet]
        [ResponseType(typeof(TransportationPriceForAdmin))]
        public IHttpActionResult GetTransportationPriceForAdmin()
        {
            try
            {
                var transportationPrice = pl.FetchTransportationPrice();
                return Ok(transportationPrice);
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occoured while getting Transportation Price"));
            }
        }

        //put method to update package details

        [HttpPut]
        [ResponseType(typeof(Package))]
        public IHttpActionResult PutPackage(int id,EditPackageDTO editPackage)
        {
            if(id == null)
            {
                return BadRequest("Package ID cannot be null");
            }
            if(editPackage == null)
            {
                return BadRequest("Provide valid Package Details to update");
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
                Package package = new Package();
                editPackage.packageid = id;
                package.PackageID = editPackage.packageid;
                package.PackageName = editPackage.packageName;
                package.PackageTypeID = editPackage.packagetypeid;
                package.NumberAvailable = editPackage.numAvailable;
                package.MaxPeople = editPackage.maximumPeople;
                package.MinPeople = editPackage.minimumPeople;
                package.Summary = editPackage.summary;
                bool editPackDetails = pl.EditPackage(package);
                if (!editPackDetails)
                {
                    return NotFound();
                }
                else
                {
                    return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.NoContent, "Successfully updated the Package Details"));
                }
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while updating the package details"));
            }  
        }

        //get method to list package name

        [HttpGet]
        [ResponseType(typeof(Package))]
        public IHttpActionResult GetPackageName()
        {
            try
            {
                var packName = pl.FetchPackageName();
                return Ok(packName);
            }
            catch
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Error occoured while getting Pscksgd"));
            }
                
        }

        //Post method to add package and itinerary details
        
        [HttpPost]
        public IHttpActionResult AddPackageType(PackageType packageType)
        { 
            if(packageType == null)
            {
                return BadRequest("Package Type cannot be Null");
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
                bool packType = pl.AddType(packageType);
                if (!packType)
                {
                    return ResponseMessage(Request.CreateErrorResponse((HttpStatusCode)422, new HttpError("Something went wrong while adding packagetypr")));
                }
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Something went wrong while adding packagetype"));
            }
            
            return Ok(packageType);
        }
        [HttpPost]
        public IHttpActionResult AddPackage()
        {
            Package package = new Package();
            Itinerary itinerary = new Itinerary();
            try
            {
                string imageName = "";
            var httpRequest = HttpContext.Current.Request;
            if(httpRequest == null)
            {
                return BadRequest("Provide valid Package Details");
            }
            if (!ModelState.IsValid)
            {
                string modelErrors = string.Join(Environment.NewLine, ModelState.Values
                                                                     .SelectMany(x => x.Errors)
                                                                     .SelectMany(x => x.ErrorMessage));
                return BadRequest(modelErrors);
            }
            var postedFile = httpRequest.Files["Image"];
            if(postedFile == null)
            {
                return BadRequest("Provide valid image File");
            }
            imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
     
            string connectionString = ConfigurationManager.ConnectionStrings["ImageBlobStorage"].ToString();

            CloudStorageAccount imageStorage = CloudStorageAccount.Parse(connectionString);

            CloudBlobClient blobClient = imageStorage.CreateCloudBlobClient();

            CloudBlobContainer container = blobClient.GetContainerReference("planyourtrippackagecontainer");

            container.CreateIfNotExists();
            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            CloudBlockBlob cloudBlockBlob = container.GetBlockBlobReference(imageName);
            cloudBlockBlob.UploadFromStream(postedFile.InputStream);
             
            package.PackageTypeID = Convert.ToInt32(httpRequest.Form["packagetypeid"]);
            package.PackageName = httpRequest.Form["packageName"];
            package.Days = Convert.ToInt32(httpRequest.Form["days"]);
            package.ProfitPercentage = Convert.ToInt32(httpRequest.Form["profitPercentage"]);
            package.Price = Convert.ToDecimal(httpRequest.Form["Price"]);
            package.Summary = httpRequest.Form["summary"];
            package.NumberAvailable = Convert.ToInt32(httpRequest.Form["numAvailable"]);
            package.MinPeople =Convert.ToInt32(httpRequest.Form["minimumPeople"]);
            package.MaxPeople =Convert.ToInt32(httpRequest.Form["maximumPeople"]);
            package.Image = cloudBlockBlob.Uri.ToString();

            int packageId =  pl.AddPackage(package);

            for(int i = 0; i < package.Days; i++)
            {
                itinerary.PackageID = packageId;
                itinerary.RoomPriceID = Convert.ToInt32(httpRequest.Form["itineraries["+ i +"].RoomPriceID"]);
                itinerary.TransportationPriceID = Convert.ToInt32(httpRequest.Form["itineraries[" + i + "].TransportationPriceID"]);
                itinerary.CityID = Convert.ToInt32(httpRequest.Form["itineraries[" + i + "].city"]);
                itinerary.ActivityDetails = httpRequest.Form["itineraries["+ i + "].activity"];
                itinerary.DayNumber = Convert.ToInt32(httpRequest.Form["itineraries[" + i + "].day"]);
                pl.AddItinerary(itinerary);
            }
            return Ok();
            }
            catch(Exception ex)
            {
                return ResponseMessage(Request.CreateErrorResponse(HttpStatusCode.InternalServerError, "Some error occoured while trying to add new Package"));
            }
        }
    }
}


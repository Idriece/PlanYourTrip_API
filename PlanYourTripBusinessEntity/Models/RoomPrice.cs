using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("RoomPrice")]
    public class RoomPrice
    {
        [Key]
        public int RoomPriceID { get; set; }
        [ForeignKey("RoomType")]
        public int RoomTypeID { get; set; }
        [ForeignKey("Hotel")]
        public int HotelID { get; set; }
        public decimal Price { get; set; }

        //Navigation Properties

        public RoomType RoomType { get; set; }
        public Hotel Hotel { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("RoomType")]
    public class RoomType
    {
        [Key]
        public int RoomTypeID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
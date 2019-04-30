using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("TransportationProvider")]
    public class TransportationProvider
    {
        [Key]
        public int TransportationProviderID { get; set; }
        [ForeignKey("City")]
        public int CityID { get; set; }
        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string TransportationProviderName { get; set; }
        [ForeignKey("ApplicationUser")]
        [Display(Name = "Owner Email Address")]
        public string Id { get; set; }

        //Navigation Properties

        public ApplicationUser ApplicationUser { get; set; }
        public City City { get; set; }
    }
}
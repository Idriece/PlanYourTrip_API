using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("City")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CityID { get; set; }
        [ForeignKey("State")]
        public int StateID { get; set; }
        [Display(Name = "City")]
        public string CityName { get; set; }

        //Navigation Properties

        public State State { get; set; }
    }
}

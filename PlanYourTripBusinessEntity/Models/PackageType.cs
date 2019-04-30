using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("PackageType")]
    public class PackageType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PackageTypeID { get; set; }

        [Column(TypeName = "VARCHAR")]
        public string Name { get; set; }
    }
}

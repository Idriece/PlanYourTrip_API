using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanYourTripBusinessEntity.Models
{
    [Table("RefundsRules")]
    public class RefundsRules
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefundRuleId { get; set; }
        public int Days { get; set; }
        public int RefundPercentage { get; set; }
    }
}

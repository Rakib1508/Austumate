using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Locations")]
    public class LocationModel
    {
        [Key]
        public string LocationID { get; set; }

        [Required]
        [Display(Name = "Location Name")]
        [StringLength(100)]
        public string LocationName { get; set; }

        [ForeignKey("CampusName")]
        public CampusModel Campus { get; set; }
        [Required]
        [Display(Name = "Campus")]
        public string CampusName { get; set; }

        [Display(Name = "Add some details")]
        public string LocationDetails { get; set; }
    }
}
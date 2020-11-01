using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Campuses")]
    public class CampusModel
    {
        [Key]
        [Display(Name = "Campus ID")]
        public string CampusID { get; set; }

        [Required]
        [Display(Name = "Name of Campus")]
        [StringLength(100)]
        public string CampusName { get; set; }

        [Required]
        [Display(Name = "Detailed Address")]
        public string Address { get; set; }
    }
}
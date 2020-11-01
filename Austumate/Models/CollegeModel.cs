using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Colleges")]
    public class CollegeModel
    {
        [Key]
        [Display(Name = "College ID")]
        [StringLength(50)]
        public string CollegeID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name of College")]
        public string CollegeName { get; set; }

        [ForeignKey("CampusName")]
        public CampusModel Campus { get; set; }
        [Required]
        [Display(Name = "Campus")]
        public string CampusName { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "College Mailbox")]
        public string CollegeMailbox { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20)]
        [Display(Name = "Office Contact number")]
        public string PhoneNumber { get; set; }
    }
}
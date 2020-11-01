using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Majors")]
    public class MajorModel
    {
        [Key]
        [Display(Name = "Major ID")]
        [StringLength(50)]
        public string MajorID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name of Major")]
        public string MajorName { get; set; }

        [ForeignKey("CollegeName")]
        public CollegeModel College { get; set; }
        [Required]
        [Display(Name = "College")]
        public string CollegeName { get; set; }

        [Required]
        [Range(1, 72, ErrorMessage = "Must be 1~72 months")]
        [Display(Name = "Time Required (in months)")]
        public int Duration { get; set; }

        [Required]
        [Display(Name = "Education Level")]
        [StringLength(50)]
        public string Level { get; set; }

        [DataType(DataType.MultilineText)]
        [StringLength(512)]
        [Display(Name = "Pre-requisites (if any)")]
        public string Requirements { get; set; }
    }
}
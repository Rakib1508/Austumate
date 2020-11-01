using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Courses")]
    public class CourseModel
    {
        [Key]
        [Display(Name = "Course ID")]
        [StringLength(50)]
        public string CourseID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Course Title")]
        public string CourseName { get; set; }

        [ForeignKey("CollegeName")]
        public CollegeModel College { get; set; }
        [Required]
        [Display(Name = "College")]
        public string CollegeName { get; set; }

        [Required]
        [Range(1, 30, ErrorMessage = "Must be 1~30")]
        [Display(Name = "Credit")]
        public int Credit { get; set; }

        [Required]
        [Display(Name = "Class Hours")]
        public int ClassHours { get; set; }

        [ForeignKey("RequiredCourse")]
        public CourseModel Course { get; set; }
        [Display(Name = "Required Course")]
        public string RequiredCourse { get; set; }
    }
}
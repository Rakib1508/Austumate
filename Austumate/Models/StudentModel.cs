using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Students")]
    public class StudentModel
    {
        [ForeignKey("ProfileID")]
        public ProfileModel Profile { get; set; }
        [Key]
        [StringLength(128)]
        [Display(Name = "User ID")]
        public string ProfileID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Student ID")]
        public string StudentID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Official Name")]
        public string StudentName { get; set; }

        [ForeignKey("MajorID")]
        public MajorModel Major { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Major")]
        public string MajorID { get; set; }

        [StringLength(255)]
        [Display(Name = "Name of Class")]
        public string ClassName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Started on")]
        public DateTime StartDate { get; set; }
    }
}
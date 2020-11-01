using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Teachers")]
    public class TeacherModel
    {
        [ForeignKey("ProfileID")]
        public ProfileModel Profile { get; set; }
        [Key]
        [StringLength(128)]
        [Display(Name = "User ID")]
        public string ProfileID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Teacher ID")]
        public string TeacherID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Official Name")]
        public string TeacherName { get; set; }

        [ForeignKey("CollegeID")]
        public CollegeModel College { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "College")]
        public string CollegeID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Joined on")]
        public DateTime JoinDate { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Official Mail")]
        public string Mailbox { get; set; }

        [Required]
        [StringLength(20)]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Official Contact Number")]
        public string PhoneNumber { get; set; }
    }
}
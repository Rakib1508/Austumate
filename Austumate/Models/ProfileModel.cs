using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.UserProfiles")]
    public class ProfileModel
    {
        [Key]
        [StringLength(128)]
        [Display(Name = "User ID")]
        public string ProfileID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Display(Name = "Middle Name")]
        public string MiddleName { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Required]
        [StringLength(10)]
        [Display(Name = "Sex")]
        public string Sex { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "National ID/Passport")]
        public string PersonID { get; set; }

        [Display(Name = "Detailed Address")]
        public string Address { get; set; }

        [StringLength(100)]
        [DataType(DataType.Url)]
        [Display(Name = "Personal Website")]
        public string Website { get; set; }

        [Display(Name = "Short Bio")]
        [DataType(DataType.MultilineText)]
        public string Bio { get; set; }
    }
}
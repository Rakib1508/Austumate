using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Registrars")]
    public class RegistrarModel
    {
        [ForeignKey("ProfileID")]
        public ProfileModel Profile { get; set; }
        [Key]
        [StringLength(128)]
        [Display(Name = "User ID")]
        public string ProfileID { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Registrar ID")]
        public string RegistrarID { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Official Name")]
        public string RegistrarName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Joined on")]
        public DateTime JoinDate { get; set; }

        [Required]
        [StringLength(255)]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Official Mail")]
        public string Mailbox { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.Semesters")]
    public class SemesterModel
    {
        [Key]
        [Display(Name = "Semester ID")]
        [StringLength(20)]
        public string SemesterID { get; set; }

        [Required]
        [Display(Name = "Session")]
        [StringLength(20)]
        public string Session { get; set; }
    }
}
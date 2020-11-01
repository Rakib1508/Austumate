using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Austumate.Models
{
    [Table("dbo.ProfileCarousels")]
    public class CarouselModel
    {
        [ForeignKey("ProfileID")]
        public ProfileModel Profile { get; set; }
        [Key]
        [StringLength(128)]
        [Display(Name = "User ID")]
        public string ProfileID { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Choose a Cover Picture")]
        public string CarouselImage { get; set; }
    }
}
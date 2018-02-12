using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcMyProject.Models
{
    public class PgModel

    {
        [Display(Name="Owner UserID")]
        [Required(ErrorMessage="*")]
        public string OBuserID { get; set; }
        [Display(Name="PG ID")]
        [Required(ErrorMessage="*")]
        public int pgID { get; set; }
        [Display(Name="PG Name")]
        [Required(ErrorMessage="*")]
        [StringLength(45,ErrorMessage="Maximum Length allowed 45")]
        public string pgName { get; set; }
        [Display(Name="PG Address")]
        [Required(ErrorMessage="*")]
        [StringLength(45, ErrorMessage = "Maximum Length allowed 45")]
        public string pgaddress { get; set; }
        [Display(Name="Pg City")]
        [Required(ErrorMessage="*")]
        public string pgcityid { get; set; }
        [Display(Name="Near By Area")]
        [Required(ErrorMessage="*")]
        [StringLength(45, ErrorMessage = "Maximum Length allowed 45")]
        public string nearbyarea { get; set; }
        [Display(Name="PG Images")]
        public string pgImagesAddress { get; set; }
        [Display(Name="Amenities")]
        [Required(ErrorMessage="*")]
        [StringLength(45, ErrorMessage = "Maximum Length allowed 45")]
        public string amenities { get; set; }
        
        [Display(Name="Pg Description")]
        [Required(ErrorMessage="*")]
        [StringLength(45, ErrorMessage = "Maximum Length allowed 45")]
        public string pgDescription { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcMyProject.Models
{
    public class RoomModel
    {
        public int roomID { get; set; }
        [Display(Name = "PG ID")]
        [Required(ErrorMessage="*")]
        public int pgID { get; set; }
        [Display(Name="Room Name")]
        [Required(ErrorMessage="*")]
        [StringLength(25, ErrorMessage = "Maximum Length allowed 25")]
        public string roomname { get; set; }
        [Display(Name="Room rent")]
        [Required(ErrorMessage="*")]
        public int roomrent { get; set; }
        [Display(Name="Room Occupancy")]
        [Required(ErrorMessage="*")]
        public int roomoccupancy { get; set; }
        [Display(Name="Room Availability")]
        [Required(ErrorMessage="*")]
        [StringLength(25, ErrorMessage = "Maximum Length allowed 25")]
        public string roomavailability { get; set; }
        [Display(Name="Room Amenities")]
        [Required(ErrorMessage="*")]
        [StringLength(25, ErrorMessage = "Maximum Length allowed 25")]
        public string roomamenities { get; set; }
        [Display(Name="Room Image 1")]
        [Required(ErrorMessage="*")]
        public string roomimage1 { get; set; }
        [Display(Name="Room Image 2")]
        [Required(ErrorMessage="*")]
        public string roomimage2 { get; set; }


        
    }
}
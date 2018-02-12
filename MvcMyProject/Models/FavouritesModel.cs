using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcMyProject.Models;
using System.ComponentModel.DataAnnotations;

namespace MvcMyProject.Models
{
    public class FavouritesModel
    {
        public int pgID { get; set; }
        public int roomID { get; set; }
        [Display(Name = "PG Name")]
        [Required(ErrorMessage = "*")]
        public string pgName { get; set; }
        [Display(Name = "PG Address")]
        [Required(ErrorMessage = "*")]
        public string pgaddress { get; set; }
        [Display(Name = "Pg City")]
        [Required(ErrorMessage = "*")]
        public string pgcityid { get; set; }
        [Display(Name = "Near By Area")]
        [Required(ErrorMessage = "*")]
        public string nearbyarea { get; set; }
        [Display(Name = "PG Images")]
        public string pgImagesAddress { get; set; }
        [Display(Name = "Amenities")]
        [Required(ErrorMessage = "*")]
        public string amenities { get; set; }
        [Display(Name = "Pg Description")]
        [Required(ErrorMessage = "*")]
        public string pgDescription { get; set; }
        [Display(Name = "Room Name")]
        [Required(ErrorMessage = "*")]
        public string roomname { get; set; }
        [Display(Name = "Room rent")]
        [Required(ErrorMessage = "*")]
        public int roomrent { get; set; }
        [Display(Name = "Room Occupancy")]
        [Required(ErrorMessage = "*")]
        public int roomoccupancy { get; set; }
        [Display(Name = "Room Availability")]
        [Required(ErrorMessage = "*")]
        public string roomavailability { get; set; }
        [Display(Name = "Room Amenities")]
        [Required(ErrorMessage = "*")]
        public string roomamenities { get; set; }
        [Display(Name = "Room Image 1")]
        [Required(ErrorMessage = "*")]
        public string roomimage1 { get; set; }
        [Display(Name = "Room Image 2")]
        [Required(ErrorMessage = "*")]
        public string roomimage2 { get; set; }
    }
}
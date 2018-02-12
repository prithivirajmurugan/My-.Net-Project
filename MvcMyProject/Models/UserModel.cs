using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcMyProject.Models
{
    public class UserModel
    {
        [Display(Name="Customer Name")]
        [Required(ErrorMessage="*")]
        [StringLength(25, ErrorMessage = "Characters below 25")]
        public string userName { get; set; }
        [Display(Name="UserType")]
        [Required(ErrorMessage="*")]
        public string userType { get; set; }
        [Display(Name="Gender")]
        [Required(ErrorMessage="*")]
        public string gender{get;set;}
        [Display(Name = "User City")]
        [Required(ErrorMessage = "*")]
        public string userCityID { get; set; }
        [Display(Name="Date Of Birth")]
        [Required(ErrorMessage="*")]
        public DateTime dateOfBirth { get; set; }
        [Display(Name="Mobile Number")]
        [Required (ErrorMessage="*")]
        [StringLength(25, ErrorMessage = "Characters below 25")]
        public string mobileNumber { get; set; }


        [Display(Name="Email ID")]
        [Required(ErrorMessage="*")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]      
        [System.Web.Mvc.Remote("CheckEmail", "Home", ErrorMessage = "Email Already Exists")]
        public string EmailId { get; set; }



        [Display(Name="Password")]
        [Required(ErrorMessage="*")]
        [StringLength(25, ErrorMessage = "Characters below 25")]
        public string password { get; set; }
        [Display(Name = "ReType Password")]
        [Required(ErrorMessage = "*")]
        [StringLength(25, ErrorMessage = "Characters below 25")]
        [Compare("password", ErrorMessage = "Password Not Matched")]
        public string reTypePassword { get; set; }
        [Display(Name = "Security Question")]
        [Required(ErrorMessage = "*")]
        [StringLength(30, ErrorMessage = "Characters below 30")]
        public string questionName { get; set; }
        [Display(Name = "Security Answer")]
        [Required(ErrorMessage="*")]
        [StringLength(30, ErrorMessage = "Characters below 30")]
        public string answer { get; set; }

    }
}
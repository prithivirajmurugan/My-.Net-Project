using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MvcMyProject.Models
{
    public class LoginModel
    {
        [Display(Name="Email ID")]
        [Required(ErrorMessage="*")]
        public string emailID { get; set; }
        [Display(Name="Password")]
        [Required(ErrorMessage="*")]
        public string password { get; set; }
        [Display(Name="Login Type")]
        [Required(ErrorMessage="*")]
        public string loginType { get; set; }
        public bool rememberMe { get; set; }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace teamwork.Models
{
    public class AdminEmail
    {
        [Required(ErrorMessage = "Required field")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "From Email")]
        public string FormEmail { get; set; }

        [Required(ErrorMessage = "Required field")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "SMTP")]
        public string SMTP { get; set; }

        [Required(ErrorMessage = "Required field")]
        [Display(Name = "Port")]
        public string Port { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace DataAccess.Models
{
    [Table("Merchant")]
    public partial class Merchant
    {

        [Key]
        public long id { get; set; }

        public UserType super_user { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Name")]
        public string merchant_name { get; set; }

        [StringLength(50)]
        public string incharge_name { get; set; }

        [StringLength(255)]
        [Display(Name = "Website URL")]
        public string url { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [StringLength(100)]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string contact_email { get; set; }

        [StringLength(20)]
        [Display(Name = "Phone")]
        public string contact_phone { get; set; }

        [StringLength(20)]
        [Display(Name = "Fax")]
        public string contact_fax { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name="Username")]
        [System.Web.Mvc.Remote("IsMerchantExists", "Remote","")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(50, ErrorMessage = "Must be between 6 and 15 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string password { get; set; }

        [NotMapped]
        [StringLength(50, ErrorMessage = "Must be between 6 and 15 characters", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Compare("password")]
        [Display(Name = "Retype Pass")]
        public string ConfirmPassword { get; set; }

        [StringLength(150)]
        public string logo { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        public short? status { get; set; }

        public short is_synched { get; set; }
    }

    public enum UserType : byte 
    {
        admin = 1,
        Merchant = 0
    }
}

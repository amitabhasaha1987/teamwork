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
        public string merchant_name { get; set; }

        [StringLength(50)]
        public string incharge_name { get; set; }

        [StringLength(255)]
        public string url { get; set; }

        [Required]
        [StringLength(100)]
        public string contact_email { get; set; }

        [StringLength(20)]
        public string contact_phone { get; set; }

        [StringLength(20)]
        public string contact_fax { get; set; }

        [Required]
        [StringLength(50)]
        public string username { get; set; }

        [Required]
        [StringLength(50)]
        public string password { get; set; }

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

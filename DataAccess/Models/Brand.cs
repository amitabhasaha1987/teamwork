namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Brand")]
    public partial class Brand
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name="Brand Name")]
        public string name { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        [Display(Name="Status")]
        public byte? display_flag { get; set; }

        public byte is_synched { get; set; }
    }
}

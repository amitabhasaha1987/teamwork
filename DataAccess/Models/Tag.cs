namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Tag")]
    public partial class Tag
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Tag Name")]
        public string name { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        [Display(Name="Status")]
        public short? display_flag { get; set; }

        public short is_synched { get; set; }
    }
}

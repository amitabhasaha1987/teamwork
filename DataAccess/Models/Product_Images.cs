namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Images
    {
        public long id { get; set; }

        public long product_id { get; set; }

        public int? color_code { get; set; }

        [StringLength(150)]
        public string file_path { get; set; }

        [StringLength(10)]
        public string extension { get; set; }

        public byte? is_trial_image { get; set; }

        public byte? is_synched { get; set; }

        public DateTime created { get; set; }

        public virtual Product Product { get; set; }
    }
}

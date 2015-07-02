namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Product_Tags
    {
        public long id { get; set; }

        public long product_id { get; set; }

        public int tag_id { get; set; }

        public DateTime created { get; set; }

        public byte is_synched { get; set; }

        public virtual Product Product { get; set; }

        public virtual Tag Tag { get; set; }
    }
}

namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Related_Products
    {
        public long id { get; set; }

        public long merchant_id { get; set; }

        public long primary_product_id { get; set; }

        public long related_product_id { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        public short? status { get; set; }

        public short is_deleted { get; set; }

        public short? is_synched { get; set; }

        public virtual Merchant Merchant { get; set; }

        public virtual Product Product { get; set; }
    }
}

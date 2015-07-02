namespace DataAccess.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;


    [Table("Product")]
    public partial class Product
    {
        public long id { get; set; }

        public long merchent_id { get; set; }

        [StringLength(100)]
        [Display(Name = "Barcode Text")]
        public string barcode { get; set; }

        [Required(ErrorMessage = "Type Required")]
        public int type { get; set; }

        [Required(ErrorMessage = "Category Required")]
        [Display(Name = "Select Product Category")]
        public int category { get; set; }

        [Required(ErrorMessage=" Product Name is required.")]
        [StringLength(100)]
        [Display(Name="Product Name")]
        public string name { get; set; }

        [Display(Name = "Select Brand")]
        public int? brand_id { get; set; }

        [Display(Name = "Sizes")]
        public int? sizes { get; set; }

        [Display(Name = "Colors")]
        public int? colors { get; set; }

        [Column(TypeName = "text")]
        [Display(Name="Description")]
        public string description { get; set; }

        [Required(ErrorMessage = " Price is required.")]
        [Display(Name = "Price")]
        public double? price { get; set; }

        [Display(Name = "Current Stock")]
        public int? current_stock { get; set; }

        [StringLength(255)]
        public string thumbnail { get; set; }

        //[StringLength(255)]
        //public string trial_image { get; set; }

        public DateTime created { get; set; }

        public DateTime? modified { get; set; }

        public byte? rating { get; set; }

        //public short is_promoted { get; set; }

        [Display(Name = "Status")]
        public byte? status { get; set; }

        [Display(Name = "Sale")]
        public byte? sale { get; set; }

        public byte is_deleted { get; set; }

        public byte is_synched { get; set; }

        [NotMapped]
        public List<Sizes> SizeList { get; set; }

        [NotMapped]
        public List<Colors> ColorsList { get; set; }

        public virtual ICollection<Product_Images> Product_Images { get; set; }

        public virtual ICollection<Product_Tags> Product_Tags { get; set; }

        public virtual ICollection<Related_Products> Related_Products { get; set; }


        public Product()
        {
           
            Product_Images = new HashSet<Product_Images>();
            Product_Tags = new HashSet<Product_Tags>();
            Related_Products = new HashSet<Related_Products>();
        

            SizeList = new List<Sizes>();
            ColorsList = new List<Colors>();
            //Populate Size
            Array sizeArray = Enum.GetValues(typeof(DataAccess.Models.enumSize));
            foreach (DataAccess.Models.enumSize val in sizeArray)
            {
                SizeList.Add(new Sizes { IsSelected = false, Size = val });
            }

            //Populate Color
            Array colorArray = Enum.GetValues(typeof(DataAccess.Models.enumColor));
            foreach (DataAccess.Models.enumColor val in colorArray)
            {
                DataAccess.Models.Colors color = new DataAccess.Models.Colors();
                color.IsSelected = false;
                color.Color = val;
                switch(val)
                {
                    case enumColor.Red: color.ColorCode = "#CC0000"; break;
                    case enumColor.LightRed: color.ColorCode = "#FF9999"; break;
                    case enumColor.Maroon: color.ColorCode = "#4C0000"; break;
                    case enumColor.Green: color.ColorCode = "#006600"; break;
                    case enumColor.LightGreen: color.ColorCode = "#7FFF7F"; break;
                    case enumColor.Blue: color.ColorCode = "#00007F"; break;
                    case enumColor.LightBlue: color.ColorCode = "#00007F"; break;
                    case enumColor.Cyan: color.ColorCode = "#00B2B2"; break;
                    case enumColor.Orange: color.ColorCode = "#FFA500"; break;
                    case enumColor.LightOrange: color.ColorCode = "#FFe4b2"; break;
                    case enumColor.Yellow: color.ColorCode = "#FFFF4C"; break;
                    case enumColor.Brown: color.ColorCode = "#AA7243"; break;
                }
                ColorsList.Add(color);
            }

            this.current_stock = 0;
            this.sale = 0;
            this.price = 0.0;
            this.merchent_id = 0;
            this.rating = 0;
            this.status = 0;
            this.created = DateTime.Now;

        }
    }

    public enum ProductType
    {
        MEN = 1,
        WOMEN = 2,
        KIDS = 3
    }

    public enum Category
    {
        Top = 1,
        Bottom = 2,
        Outwear = 3,
        Shoes = 4,
        Dress = 5
    }

    public enum enumSize
    {
        S = 1,
        M = 2,
        L = 4,
        XL = 8,
        XXL = 16
    }

    public enum enumColor
    {
        Red = 1,
        [Display(Name="Light Red")]
        LightRed = 2,
        Maroon = 4,
        Green = 8,
        [Display(Name="Light Green")]
        LightGreen =16,
        Blue = 32,
        [Display(Name="Light Blue")]
        LightBlue = 64,
        Cyan = 128,
        Orange =256,
        [Display(Name="Light Orange")]
        LightOrange = 512,
        Yellow = 1024,
        Brown = 2048,
        //[Display(Name="Light Brown")]
        //LightBrown = 4096,
        //Pink = 8192,
        //[Display(Name="Light Pink")]
        //LightPink = 16384
    }

    public class Colors
    {
        public bool IsSelected { get; set; }
        public string ColorCode { get; set; }
        public enumColor Color { get; set; }
    }

    public class Sizes
    {
        public bool IsSelected { get; set; }
        public enumSize Size { get; set; }
    }



}

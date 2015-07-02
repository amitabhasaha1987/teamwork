using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Models;


namespace DataAccess
{
    public class Context : DbContext
    {
        public Context()
            : base("StylisticsAdmin")
        {
        }

        public virtual DbSet<Merchant> Merchants { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
               .Property(e => e.merchant_name)
               .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.incharge_name)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.url)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.contact_email)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.contact_phone)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.contact_fax)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.username)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.password)
                .IsUnicode(false);

            modelBuilder.Entity<Merchant>()
                .Property(e => e.logo)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.name)
                .IsUnicode(false);

            /* Product */
            modelBuilder.Entity<Product>()
                .Property(e => e.barcode)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.name)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .Property(e => e.thumbnail)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                            .HasMany(e => e.Product_Images)
                            .WithRequired(e => e.Product)
                            .HasForeignKey(e => e.product_id)
                            .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Product_Tags)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Related_Products)
                .WithRequired(e => e.Product)
                .HasForeignKey(e => e.primary_product_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product_Images>()
                .Property(e => e.extension)
                .IsFixedLength();
        }




    }
}

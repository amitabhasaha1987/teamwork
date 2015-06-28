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
        }

       
        
       
    }
}

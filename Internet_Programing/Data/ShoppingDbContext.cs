using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internet_Programing.Models;

namespace Internet_Programing.Data
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Internet_Programing.Models.Products> Product { get; set; }
        public DbSet<Internet_Programing.Models.OS> OS { get; set; }
        public DbSet<Internet_Programing.Models.Customer> Customer { get; set; }
        public DbSet<Internet_Programing.Models.CartProduct> CartProduct { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartProduct>()
                .HasKey(cp => new { cp.ProductsId, cp.CustomerId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Products)
                .WithMany(p => p.Cart)
                .HasForeignKey(cp => cp.ProductsId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartProduct>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.Cart)
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CartProduct>().

            base.OnModelCreating(modelBuilder);
        }

        


    }
}

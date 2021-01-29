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

        public DbSet<Internet_Programing.Models.Phone> Phone { get; set; }
        public DbSet<Internet_Programing.Models.OS> OS { get; set; }
        public DbSet<Internet_Programing.Models.Customer> Customer { get; set; }
        public DbSet<Internet_Programing.Models.CartPhone> CartProduct { get; set; }

        public DbSet<Internet_Programing.Models.Brand> Brand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartPhone>()
                .HasKey(cp => new { cp.PhoneId, cp.CustomerId });

            modelBuilder.Entity<CartPhone>()
                .HasOne(cp => cp.Phone)
                .WithMany(p => p.Cart)
                .HasForeignKey(cp => cp.PhoneId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CartPhone>()
                .HasOne(cp => cp.Customer)
                .WithMany(c => c.Cart)
                .HasForeignKey(cp => cp.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CartProduct>().

            base.OnModelCreating(modelBuilder);
        }

        


    }
}

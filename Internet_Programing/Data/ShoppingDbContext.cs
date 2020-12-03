using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Data
{
    public class ShoppingDbContext : DbContext
    {
        public ShoppingDbContext(DbContextOptions<ShoppingDbContext> options)
            : base(options)
        {
        }

        public DbSet<Internet_Programing.Models.Products> Product { get; set; }
        //PUT HERE another sets and make migrations process
        // Add migrations
        // Update database

    }
}

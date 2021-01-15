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


    }
}

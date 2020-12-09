using Internet_Programing.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class SeedData
    {
        internal static void Populate(ShoppingDbContext dbContext)
        {
            PopulateProducts(dbContext);
        }

        private static void PopulateProducts(ShoppingDbContext dbContext)
        {
            if (dbContext.Product.Any())
            {
                return;
            }

            dbContext.Product.AddRange(
                new Products
                {
                    Name = "Kayak",
                    Description = "A boat for one person",
                    OS = "Watersports",
                    Price = 275,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Lifejacket",
                    Description = "Protective and fashionable",
                    OS = "Watersports",
                    Price = 48,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Soccer Ball",
                    Description = "FIFA-approved size and weight",
                    OS = "Soccer",
                    Price = 19,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Corner Flags",
                    Description = "Give your playing field a professional touch",
                    OS = "Soccer",
                    Price = 34,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Stadium",
                    Description = "Flat-packed 35,000-seat stadium",
                    OS = "Soccer",
                    Price = 79500,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Thinking Cap",
                    Description = "Improve brain efficiency by 75%",
                    OS = "Chess",
                    Price = 16,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Unsteady Chair",
                    Description = "Secretly give your opponent a disadvantage",
                    OS = "Chess",
                    Price = 29,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Human Chess Board",
                    Description = "A fun game for the family",
                    OS = "Chess",
                    Price = 75,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                },
                new Products
                {
                    Name = "Bling-Bling King",
                    Description = "Gold-plated, diamond-studded King",
                    OS = "Chess",
                    Price = 1200,
                    BatteryAmpere = 20,
                    RAM = 3,
                    Memory = 4,
                    Processor = "dasda"
                }
            );

            dbContext.SaveChanges();
        }
    }
}

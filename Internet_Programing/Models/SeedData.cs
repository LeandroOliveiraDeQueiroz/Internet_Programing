using Internet_Programing.Data;
using Microsoft.EntityFrameworkCore;
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
            OS Os1 = new OS
            {
               Name = "Android",
               Version = 10
            };
            OS Os2 = new OS
            {
                Name = "iOS",
                Version = 10
            };

            PopulateOS(dbContext, Os1, Os2);
            PopulateProductsAsync(dbContext, null, null).Wait();
        }

        private static void PopulateOS(ShoppingDbContext dbContext, OS Os1, OS Os2)
        {
            if (dbContext.OS.Any())
            {
                return;
            }

            dbContext.OS.AddRange(
                Os1, 
                Os2
            );

            dbContext.SaveChanges();
        }

        private static async Task PopulateProductsAsync(ShoppingDbContext dbContext, OS Os1, OS Os2)
        {
            if (dbContext.Product.Any())
            {
                return;
            }

            if (dbContext.OS.Any())
            {
                var x = await dbContext.OS.ToListAsync();

                int AndroidId = x[0].OSId;
                int iOSID = x[1].OSId;
            
                dbContext.Product.AddRange(
                new Products
                {
                    Name = "Apple iPhone 12",
                    Description = "",
                    OS = Os2,
                    OSId = iOSID,
                    Price = 500,
                    BatteryAmpere = 2815,
                    RAM = 4,
                    Memory = 256,
                    Processor = "2x 2.65 GHz Firestorm + x 1.8 GHz Icestorm"
                },
                new Products
                {
                   Name = "Samsung Galaxy S20 Ultra",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500,
                    BatteryAmpere = 5000,
                    RAM = 12,
                    Memory = 128,
                    Processor = "2x 2.73 GHz Mongoose M5 + 2x 2.4 GHz Cortex-A76 + 4x 1.9 GHz Cortex-A55"
                },
                new Products
                {
                    Name = "Samsung Galaxy A51",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500,
                    BatteryAmpere = 4000,
                    RAM = 4,
                    Memory = 128,
                    Processor = "4x 2.3 GHz Cortex-A73 + 4x 1.7 GHz Cortex-A53"
                },
                new Products
                {
                    Name = "Redmi Note 9S",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500,
                    BatteryAmpere = 5020,
                    RAM = 4,
                    Memory = 64,
                    Processor = "2x 2.3 GHz Kryo 465 Gold + 6x 1.8 GHz Kryo 465 Silver"
                },
                new Products
                {
                    Name = "Samsung Galaxy S10",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500,
                    BatteryAmpere = 3400,
                    RAM = 128,
                    Memory = 8,
                    Processor = "4x 1.95 GHz Cortex-A55 + 2x 2.3 GHz Cortex-A75 + 2x 2.7 GHz M4"
                },
                new Products
                {
                    Name = "Samsung Galaxy M31",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500,
                    BatteryAmpere = 6000,
                    RAM = 6,
                    Memory = 128,
                    Processor = "4x 2.3 GHz Cortex-A73 + 4x 1.7 GHz Cortex-A53"
                },
                new Products
                {
                    Name = "Motorola Moto G9 Plus",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500,
                    BatteryAmpere = 5000,
                    RAM = 4,
                    Memory = 128,
                    Processor = "2x 2.2 GHz Kryo 470 Gold + 6x 1.8 GHz Kryo 470 Silver"
                },
                new Products
                {
                    Name = "Motorola Edge",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500,
                    BatteryAmpere = 4500,
                    RAM = 6,
                    Memory = 128,
                    Processor = "1x 2.4 GHz Kryo 475 Prime + 1x 2.2 GHz Kryo 475 Gold + 6 x1.8 GHz Kryo 475 Silver"
                },
                new Products
                {
                    Name = "Apple iPhone XR",
                    Description = "",
                    OS = Os2,
                    OSId = iOSID,
                    Price = 500,
                    BatteryAmpere = 2942,
                    RAM = 3,
                    Memory = 256,
                    Processor = "2x 2.5 GHz Vortex + 4x1.6 GHz Tempest"
                }
            );

            dbContext.SaveChanges();
            }
        }
    }
}

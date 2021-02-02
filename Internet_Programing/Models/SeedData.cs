using Internet_Programing.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Internet_Programing.Models
{
    public class SeedData
    {
        private const string ROLE_ADMIN = "admin";
        private const string ROLE_CUSTOMER = "customer";
        private const string ROLE_PRODUCT_MANAGER = "productManager";

        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await EnsureRoleIsCreatedAsync(roleManager, ROLE_ADMIN);
            await EnsureRoleIsCreatedAsync(roleManager, ROLE_CUSTOMER);
            await EnsureRoleIsCreatedAsync(roleManager, ROLE_PRODUCT_MANAGER);
        }

        private static async Task EnsureRoleIsCreatedAsync(RoleManager<IdentityRole> roleManager, string role)
        {
            if (await roleManager.RoleExistsAsync(role)) return;

            await roleManager.CreateAsync(new IdentityRole(role));
        }

        public static async Task SeedAdminAsync(UserManager<IdentityUser> userManager)
        {
            const string adminUser = "admin@wanted.pt";
            const string adminPass = "Secret123$";

            IdentityUser user = await EnsureUserIsCreatedAsync(userManager, adminUser, adminPass);
            await EnsureUserIsInRole(userManager, user, ROLE_ADMIN);
        }

        private static async Task<IdentityUser> EnsureUserIsCreatedAsync(UserManager<IdentityUser> userManager, string username, string password)
        {
            IdentityUser user = await userManager.FindByNameAsync(username);

            if(user == null)
            {
                user = new IdentityUser { UserName = username, Email = username };
                IdentityResult result = await userManager.CreateAsync(user, password);
            }

            return user;
        }

        private static async Task EnsureUserIsInRole(UserManager<IdentityUser> userManager, IdentityUser user, string role)
        {
            if (await userManager.IsInRoleAsync(user, role)) return;

            await userManager.AddToRoleAsync(user, role);
        }

        public static async Task SeedDevUsersAsync(UserManager<IdentityUser> userManager)
        {
            const string customerUser = "customer@wanted.pt";
            const string customerPassword = "Secret123$";

            const string productManagerUser = "manager@wanted.pt";
            const string productManagerPassword = "Secret123$";

            IdentityUser user = await EnsureUserIsCreatedAsync(userManager, customerUser, customerPassword);
            await EnsureUserIsInRole(userManager, user, ROLE_CUSTOMER);

            user = await EnsureUserIsCreatedAsync(userManager, productManagerUser, productManagerPassword);
            await EnsureUserIsInRole(userManager, user, ROLE_PRODUCT_MANAGER);
        }


        public static void Populate(ShoppingDbContext dbContext)
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
            PopulateBrand(dbContext);
            PopulatePhonesAsync(dbContext, null, null).Wait();
            PopulateCustomers(dbContext);
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

        private static void PopulateBrand(ShoppingDbContext dbContext)
        {
            if (dbContext.Brand.Any())
            {
                return;
            }

            dbContext.Brand.AddRange(
                new Brand 
                {
                    Name="Apple"
                },
                new Brand
                {
                    Name = "Samsung"
                },
                new Brand
                {
                    Name = "Xiaomi"
                },
                new Brand
                {
                    Name = "Motorola"
                }
            );

            dbContext.SaveChanges();
        }

        private static async Task PopulatePhonesAsync(ShoppingDbContext dbContext, OS Os1, OS Os2)
        {
            if (dbContext.Phone.Any())
            {
                return;
            }

            if (dbContext.OS.Any() && dbContext.Brand.Any())
            {
                var x = await dbContext.OS.ToListAsync();

                int AndroidId = x[0].OSId;
                int iOSID = x[1].OSId;

                var brands = await dbContext.Brand.ToListAsync();

                var AppleBrand = brands.Find(b => b.Name == "Apple");
                var SamsungBrand = brands.Find(b => b.Name == "Samsung");
                var XiaomiBrand = brands.Find(b => b.Name == "Xiaomi");
                var MotorolaBrand = brands.Find(b => b.Name == "Motorola");

                dbContext.Phone.AddRange(
                new Phone
                {
                    Name = "Apple iPhone 12",
                    Description = "",
                    OS = Os2,
                    OSId = iOSID,
                    Price = 800.30m,
                    BatteryAmpere = 2815,
                    RAM = 4,
                    Memory = 256,
                    Processor = "2x 2.65 GHz Firestorm + x 1.8 GHz Icestorm",
                    Photo  = System.IO.File.ReadAllBytes("./wwwroot/assets/iphone12.jfif"),
                    Brand = AppleBrand,
                    BrandId = AppleBrand.BrandId
                },
                new Phone
                {
                   Name = "Samsung Galaxy S20 Ultra",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 752.20m,
                    BatteryAmpere = 5000,
                    RAM = 12,
                    Memory = 128,
                    Processor = "2x 2.73 GHz Mongoose M5 + 2x 2.4 GHz Cortex-A76 + 4x 1.9 GHz Cortex-A55",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/SamsungGalaxyS20Ultra.jfif"),
                    Brand = SamsungBrand,
                    BrandId = SamsungBrand.BrandId
                },
                new Phone
                {
                    Name = "Samsung Galaxy A51",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 400.99m,
                    BatteryAmpere = 4000,
                    RAM = 4,
                    Memory = 128,
                    Processor = "4x 2.3 GHz Cortex-A73 + 4x 1.7 GHz Cortex-A53",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/SamsungGalaxyA51.jfif"),
                    Brand = SamsungBrand,
                    BrandId = SamsungBrand.BrandId
                },
                new Phone
                {
                    Name = "Redmi Note 9S",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500.50m,
                    BatteryAmpere = 5020,
                    RAM = 4,
                    Memory = 64,
                    Processor = "2x 2.3 GHz Kryo 465 Gold + 6x 1.8 GHz Kryo 465 Silver",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/RedmiNote9S.jfif"),
                    Brand = XiaomiBrand,
                    BrandId = XiaomiBrand.BrandId
                },
                new Phone
                {
                    Name = "Samsung Galaxy S10",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 500.30m,
                    BatteryAmpere = 3400,
                    RAM = 128,
                    Memory = 8,
                    Processor = "4x 1.95 GHz Cortex-A55 + 2x 2.3 GHz Cortex-A75 + 2x 2.7 GHz M4",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/SamsungGalaxyS10.jfif"),
                    Brand = SamsungBrand,
                    BrandId = SamsungBrand.BrandId
                },
                new Phone
                {
                    Name = "Samsung Galaxy M31",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 350.99m,
                    BatteryAmpere = 6000,
                    RAM = 6,
                    Memory = 128,
                    Processor = "4x 2.3 GHz Cortex-A73 + 4x 1.7 GHz Cortex-A53",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/SamsungGalaxyM31.jfif"),
                    Brand = SamsungBrand,
                    BrandId = SamsungBrand.BrandId
                },
                new Phone
                {
                    Name = "Motorola Moto G9 Plus",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 480.00m,
                    BatteryAmpere = 5000,
                    RAM = 4,
                    Memory = 128,
                    Processor = "2x 2.2 GHz Kryo 470 Gold + 6x 1.8 GHz Kryo 470 Silver",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/MotorolaMotoG9Plus.jfif"),
                    Brand = MotorolaBrand,
                    BrandId = MotorolaBrand.BrandId
                },
                new Phone
                {
                    Name = "Motorola Edge",
                    Description = "",
                    OS = Os1,
                    OSId = AndroidId,
                    Price = 599.99m,
                    BatteryAmpere = 4500,
                    RAM = 6,
                    Memory = 128,
                    Processor = "1x 2.4 GHz Kryo 475 Prime + 1x 2.2 GHz Kryo 475 Gold + 6 x1.8 GHz Kryo 475 Silver",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/MotorolaEdge.jfif"),
                    Brand = MotorolaBrand,
                    BrandId = MotorolaBrand.BrandId
                },
                new Phone
                {
                    Name = "Apple iPhone XR",
                    Description = "",
                    OS = Os2,
                    OSId = iOSID,
                    Price = 699.99m,
                    BatteryAmpere = 2942,
                    RAM = 3,
                    Memory = 256,
                    Processor = "2x 2.5 GHz Vortex + 4x1.6 GHz Tempest",
                    Photo = System.IO.File.ReadAllBytes("./wwwroot/assets/AppleiPhoneXR.jfif"),
                    Brand = AppleBrand,
                    BrandId = AppleBrand.BrandId
                }
            );

            dbContext.SaveChanges();
            }
        }

        private static void PopulateCustomers(ShoppingDbContext dbContext)
        {
            if (dbContext.Customer.Any()) return;

            dbContext.Customer.Add(new Customer
            {
                Name = "Leandro",
                Email = "customer@wanted.pt"
            });

            dbContext.SaveChanges();
        }
    }
}

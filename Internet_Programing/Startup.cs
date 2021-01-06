using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Internet_Programing.Data;
using Internet_Programing.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Internet_Programing
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ShoppingDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ShoppingConnection")));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("ShoppingUsersConnection")));
            services.AddIdentity<IdentityUser, IdentityRole>( options => {
                //Sign In
                options.SignIn.RequireConfirmedAccount = false;
                
                //Password
               options.Password.RequireDigit = true;
               options.Password.RequiredLength = 8;
               options.Password.RequireDigit = true;
               options.Password.RequireLowercase = true;
               options.Password.RequireUppercase = true;

                //Lockout
                    
            }).AddEntityFrameworkStores<ApplicationDbContext>().
               AddDefaultUI();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddTransient<ShoppingRepository, EntityFrameworkRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetService<ShoppingDbContext>();
                    SeedData.Populate(dbContext);
                    SeedData.SeedRoles(roleManager).Wait();
                    SeedData.SeedAdminAsync(userManager).Wait();
                    SeedData.SeedDevUsersAsync(userManager).Wait();
                }
            }
        }
    }
}

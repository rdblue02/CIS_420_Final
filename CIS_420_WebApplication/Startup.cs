using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CIS_420_WebApplication.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authorization;
using CIS_420_WebApplication.Models;
using Microsoft.AspNetCore.Mvc.Razor;


namespace CIS_420_WebApplication
{
    public class Startup
    {
       
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {               
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;        
                          });

              services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddIdentity<ApplicationUser,IdentityRole>(options=>
            {
                options.User.RequireUniqueEmail = true;               
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;      
              
            })
                .AddDefaultTokenProviders()
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddAuthorization();
            services.AddAuthentication();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);         
        }
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,IServiceProvider provider,ApplicationDbContext context)
        {
            SeedDataBase seedDataBase = new SeedDataBase(provider, context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            
            app.UseAuthentication();
                           
            try
            {
                seedDataBase.Seed().Wait();
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=Index}/{id?}");                  
                });           
            }
            catch
            {
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Home}/{action=DBError}");
                });
            }              
        }
    }
}

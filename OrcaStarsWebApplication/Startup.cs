using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrcaStarsWebApplication.Repositories;
using OrcaStarsWebApplication.Services;
using OrcaStarsWebApplication.Models;
using Microsoft.AspNetCore.Identity;
using OrcaStarsWebApplication.Data;
using OrcaStarsWebApplication.Areas.Identity.Data;

namespace OrcaStarsWebApplication
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
            services.AddTransient<IBusinessRepository, BusinessRepository>();
            services.AddTransient<IBusinessServices, BusinessServices>();
            services.AddTransient<AddressService>();
            services.AddTransient<HoursService>();
            services.AddTransient<PhoneService>();
            services.AddMvc(options => options.EnableEndpointRouting = false); //added disable endpoint routing
            services.AddDbContext<Models.BitDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OrcaStarsWebApplication-Win-SqlServer"));
                //options.UseSqlite(Configuration.GetConnectionString("OrcaStarsWebApplication-Mac-Sqlite"));
            });

            services.Configure<IdentityOptions>(options =>
            {
                //password settings
                //lockout settings
                //user settings
            }); 

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Admin/Error");
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
                    pattern: "{controller=Admin}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });

            CreateRoles(serviceProvider).Wait();//.GetAwaiter().GetResult();
        }
        
        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<OrcaStarsWebApplicationUser>>();
            string[] roleNames = { "Admin", "Member" };
            IdentityResult roleResult;

            foreach (var roleName in roleNames)
            {
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            var user = new OrcaStarsWebApplicationUser();
            //if (await UserManager.GetRolesAsync(user) == null)
            //{
            //    await UserManager.AddToRoleAsync(user, "Member");
            //}

            var _admin = await UserManager.FindByEmailAsync("maddie@orca.com");
            if (_admin == null)
            {
                var admin = new OrcaStarsWebApplicationUser
                {
                    UserName = "maddie@orca.com",
                    Email = "maddie@orca.com"
                };

                var createAdmin = await UserManager.CreateAsync(admin, "Admin!");
                
                if (createAdmin.Succeeded)
                {
                    await UserManager.AddToRoleAsync(admin, "Admin");
                }     
            }
            else
            {
                await UserManager.AddToRoleAsync(_admin, "Admin");
            }

            var _member = await UserManager.FindByEmailAsync("orca@orcastars.com");
            if (_member == null)
            {
                var member = new OrcaStarsWebApplicationUser
                {
                    UserName = "orca@orcastars.com",
                    Email = "orca@orcastar.com"
                };

                var createMember = await UserManager.CreateAsync(member, "member!");
                
                if (createMember.Succeeded)
                {
                    await UserManager.AddToRoleAsync(member, "Member");
                }  
            }
            else
            {
                await UserManager.AddToRoleAsync(_member, "Member");
            }
        }
    }
}

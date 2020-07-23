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
            services.AddMvc(options => options.EnableEndpointRouting = false); //added disable endpoint routing
            services.AddDbContext<Models.BitDataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("OrcaStarsWebApplication-Win-SqlServer"));
                //options.UseSqlite(Configuration.GetConnectionString("OrcaStarsWebApplication-Mac-Sqlite"));
            });


            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
        }
    }
}

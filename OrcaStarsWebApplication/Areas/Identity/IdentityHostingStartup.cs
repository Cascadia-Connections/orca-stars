using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrcaStarsWebApplication.Areas.Identity.Data;
using OrcaStarsWebApplication.Data;

[assembly: HostingStartup(typeof(OrcaStarsWebApplication.Areas.Identity.IdentityHostingStartup))]
namespace OrcaStarsWebApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<OrcaStarsWebApplicationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("OrcaStarsWebApplicationContextConnection")));

                services.AddDefaultIdentity<OrcaStarsWebApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<OrcaStarsWebApplicationContext>();
            });
        }
    }
}
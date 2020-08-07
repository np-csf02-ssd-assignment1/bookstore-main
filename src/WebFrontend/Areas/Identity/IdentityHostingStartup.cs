using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebFrontend.Areas.Identity.Data;
using WebFrontend.Model;

[assembly: HostingStartup(typeof(WebFrontend.Areas.Identity.IdentityHostingStartup))]
namespace WebFrontend.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<WebFrontendIdentityDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("WebFrontendIdentityDbContextConnection")));

                services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddRoles<ApplicationRole>()
                    .AddEntityFrameworkStores<WebFrontendIdentityDbContext>()
                    .AddDefaultTokenProviders();
            });
        }
    }
}
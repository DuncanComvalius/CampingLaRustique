using System;
using CampingLaRustique.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(CampingLaRustique.Areas.Identity.IdentityHostingStartup))]
namespace CampingLaRustique.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<CampingLaRustiqueContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("CampingLaRustiqueContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<CampingLaRustiqueContext>();
            });
        }
    }
}
using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ModernMarketTM.Web.Areas.Identity.IdentityHostingStartup))]
namespace ModernMarketTM.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {});
        }
    }
}
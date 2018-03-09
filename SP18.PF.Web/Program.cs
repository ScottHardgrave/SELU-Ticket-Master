using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SP18.PF.Web.Data;

namespace SP18.PF.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<DbContext>();
                context.Database.Migrate();
                DbInitilizer.SeedData(context);
            }
            host.Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}

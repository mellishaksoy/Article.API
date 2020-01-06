using Article.API.Infrastructure.Extensions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Serilog;

namespace Article.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WebHost
                .CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(
                    (hostingContext, builder) =>
                    {
                        builder
                            .AddEnvironmentVariables();
                    })
                .UseSerilog()
                .UseStartup<Startup>()
                .Build()
                .SeedData()
                .Run();
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

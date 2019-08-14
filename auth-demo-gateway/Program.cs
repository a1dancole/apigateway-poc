using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace auth_demo_gateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args)
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, config) =>
                {
                    config
                        .SetBasePath(context.HostingEnvironment.ContentRootPath)
                        .AddJsonFile("appsettings.json", true, true)
                        .AddJsonFile($"appsettings.{context.HostingEnvironment.EnvironmentName}.json",
                            true, true)
                        .AddJsonFile("ocelot.json")
                        .AddEnvironmentVariables();
                })

                .ConfigureLogging((hostingContext, logging) =>
                {
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                })
                .UseIISIntegration()
                .Build()
                .Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}

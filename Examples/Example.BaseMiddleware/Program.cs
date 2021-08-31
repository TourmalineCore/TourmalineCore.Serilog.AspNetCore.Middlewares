using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using TourmalineCore.Serilog.AspNetCore.Middlewares.Extensions;

namespace Example.BaseMiddleware
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = Host.CreateDefaultBuilder(args);

            return builder
                .UseTourmalineCoreLogging("logging-test")
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
        }
    }
}
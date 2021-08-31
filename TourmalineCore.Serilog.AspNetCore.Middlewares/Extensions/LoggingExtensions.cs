using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using TourmalineCore.Serilog.AspNetCore.Middlewares.Contracts;
using TourmalineCore.Serilog.AspNetCore.Middlewares.DefaultContractsRealization;
using TourmalineCore.Serilog.AspNetCore.Middlewares.Middlewares;
using TourmalineCore.Serilog.Formatting.Tiny.Formatters.Extensions;

namespace TourmalineCore.Serilog.AspNetCore.Middlewares.Extensions
{
    public static class LoggingExtensions
    {
        public static IServiceCollection AddTourmalineCoreLoggingWithMiddlewares(this IServiceCollection services)
        {
            return services
                .AddTransient<ILoggingValuesGenerator, LoggingValuesGenerator>()
                .AddLogging(loggingBuilder => loggingBuilder.AddSerilog(dispose: true));
        }

        public static IServiceCollection AddLoggingValuesGenerator<TLoggingValuesGenerator>(this IServiceCollection services)
            where TLoggingValuesGenerator : ILoggingValuesGenerator
        {
            return services.AddTransient(typeof(ILoggingValuesGenerator), typeof(TLoggingValuesGenerator));
        }

        public static IApplicationBuilder UseTourmalineCoreLogging(this IApplicationBuilder applicationBuilder)
        {
            return applicationBuilder
                .UseSerilogRequestLogging()
                .UseMiddleware<LoggingMiddleware>();
        }

        public static IWebHostBuilder UseTourmalineCoreLogging(this IWebHostBuilder builder, string environmentName, string applicationName)
        {
            return builder.UseSerilog((hostingContext, loggerConfiguration) => loggerConfiguration.FromConfig(hostingContext.Configuration, environmentName, applicationName));
        }

        public static IHostBuilder UseTourmalineCoreLogging(this IHostBuilder builder, string applicationName)
        {
            return builder.UseSerilog((hostingContext, loggerConfiguration) =>
                    loggerConfiguration.FromConfig(
                            hostingContext.Configuration,
                            hostingContext.HostingEnvironment.EnvironmentName,
                            applicationName
                        )
                );
        }
    }
}
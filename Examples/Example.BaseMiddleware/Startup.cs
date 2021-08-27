using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TourmalineCore.Logging.Extensions.AspNetCore.Extensions;

namespace Example.BaseMiddleware
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddTourmalineCoreLogging()
                .AddLoggingValuesGenerator<LoggingValuesGenerator>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(
                builder => builder
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(host => true)
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
            );

            app.UseRouting();

            app.UseTourmalineCoreLogging();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

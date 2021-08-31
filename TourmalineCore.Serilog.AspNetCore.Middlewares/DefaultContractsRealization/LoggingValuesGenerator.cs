using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TourmalineCore.Serilog.AspNetCore.Middlewares.Contracts;

namespace TourmalineCore.Serilog.AspNetCore.Middlewares.DefaultContractsRealization
{
    public class LoggingValuesGenerator : ILoggingValuesGenerator
    {
        public Task<Dictionary<string, string>> Execute(HttpContext httpContext)
        {
            return Task.FromResult(new Dictionary<string, string>());
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using TourmalineCore.Logging.Extensions.AspNetCore.Contracts;

namespace Example.BaseMiddleware
{
    public class LoggingValuesGenerator : ILoggingValuesGenerator
    {
        public Task<Dictionary<string, string>> Execute(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            if (!httpContext.Request.Body.CanSeek)
            {
                return Task.FromResult(new Dictionary<string, string>());
            }

            return Task.FromResult(new Dictionary<string, string>
            {
                { "login", "login" },
                { "phoneNumber", "login" }
            });
        }
    }
}
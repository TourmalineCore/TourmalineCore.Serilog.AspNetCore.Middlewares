using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace TourmalineCore.Serilog.AspNetCore.Middlewares.Contracts
{
    public interface ILoggingValuesGenerator
    {
        public Task<Dictionary<string, string>> Execute(HttpContext httpContext);
    }
}
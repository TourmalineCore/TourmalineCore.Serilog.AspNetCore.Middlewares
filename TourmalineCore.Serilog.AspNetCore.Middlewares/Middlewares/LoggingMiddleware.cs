using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;
using TourmalineCore.Serilog.AspNetCore.Middlewares.Contracts;

namespace TourmalineCore.Serilog.AspNetCore.Middlewares.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;
        private readonly IDiagnosticContext _diagnosticContext;
        private readonly ILoggingValuesGenerator _loggingValuesGenerator;

        public LoggingMiddleware(
            RequestDelegate next,
            ILogger<LoggingMiddleware> logger,
            IDiagnosticContext diagnosticContext,
            ILoggingValuesGenerator loggingValuesGenerator)
        {
            _next = next;
            _logger = logger;
            _diagnosticContext = diagnosticContext;
            _loggingValuesGenerator = loggingValuesGenerator;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Request.EnableBuffering();

            var values = await _loggingValuesGenerator.Execute(httpContext);

            await ExecuteNextNextWithEnrich(httpContext, values);
        }

        private async Task ExecuteNextNextWithEnrich(HttpContext httpContext, Dictionary<string, string> loggingValues)
        {
            if (loggingValues.Any())
            {
                foreach (var (key, value) in loggingValues)
                {
                    _diagnosticContext.Set(key, value);
                }

                var enrichers = loggingValues
                    .Select(pair => new PropertyEnricher(pair.Key, pair.Value))
                    .ToArray<ILogEventEnricher>();

                using (LogContext.Push(enrichers))
                {
                    await _next(httpContext);
                }

                return;
            }

            await _next(httpContext);
        }
    }
}
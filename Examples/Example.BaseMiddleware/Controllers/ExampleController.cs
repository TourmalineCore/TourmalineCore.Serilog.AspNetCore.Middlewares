using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Example.BaseMiddleware.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;

        public ExampleController(ILogger<ExampleController> logger)
        {
            _logger = logger;
        }

        /*
         * This is the endpoint for verifying that the logging middleware is working correctly.
         * You should see this log message once in your console application when calling this endpoint.
         * If you have seen this log message multiple times, it means that the middleware is not working correctly.
         */
        [HttpPost("check-middleware-correct-work")]
        public void CheckMiddlewareCorrectWork()
        {
            _logger.LogInformation("Please make sure this log message is called once. If you see this message multiple times, the middleware is not working correctly");
        }
    }
}
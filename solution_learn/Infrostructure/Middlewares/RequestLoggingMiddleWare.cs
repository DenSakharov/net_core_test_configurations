using Microsoft.AspNetCore.Identity.Data;
using System.Reflection;
using System.Text;

namespace solution_learn.Infrostructure.Middlewares
{
    public class RequestLoggingMiddleWare
    {
        private RequestDelegate _next;
        private readonly ILogger _logger;
        public RequestLoggingMiddleWare(RequestDelegate next, ILogger<RequestLoggingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            await LogRequest(context);
            await _next(context);
        }

        private async Task LogRequest(HttpContext context)
        {
            try
            {
                if (context.Request.ContentLength > 0)
                {
                    context.Request.EnableBuffering();

                    var buffer = new byte[context.Request.ContentLength.Value];
                    await context.Request.Body.ReadAsync(buffer, 0, buffer.Length);
                    var bodyAsText = Encoding.UTF8.GetString(buffer);

                    _logger.LogInformation("Request logged");
                    _logger.LogInformation(bodyAsText);

                    context.Request.Body.Position = 0;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Could not log request body");
                //Console.WriteLine(ex.ToString());   
            }
        }
    }
}

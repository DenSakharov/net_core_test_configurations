using System.Reflection;

namespace solution_learn.Infrostructure.Middlewares
{
    public class VersionMiddleware
    {
        public VersionMiddleware(RequestDelegate next)
        {

        }
        public async Task InvokeAsync(HttpContext context)
        {
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString();
            context.Response.WriteAsync(version);
        }
    }
}

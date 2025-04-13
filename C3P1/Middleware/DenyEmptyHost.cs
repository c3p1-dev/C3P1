namespace C3P1.Middleware
{
    public class DenyEmptyHost
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<DenyEmptyHost> _logger;

        public DenyEmptyHost(RequestDelegate next, ILogger<DenyEmptyHost> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            // deny connections with missing header Host before trying to manage exceptions
            if (string.IsNullOrEmpty(context.Request.Host.Host))
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad Request : Missing Host header");
                return;
            }

            await _next(context);
        }
    }

    // usage
    // app.UseMiddleware<UriLoggingMiddleware>();
    // in Program.cs, before classic Middlewares
}

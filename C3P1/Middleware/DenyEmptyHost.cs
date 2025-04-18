namespace C3P1.Middleware
{
    // Usage :
    // app.UseMiddleware<UriLoggingMiddleware>();
    // in Program.cs, before classic Middlewares
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
}

/* Middleware for exception hunt
 * Old code for memory
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (UriFormatException ex)
    {
        Console.WriteLine("======================================");
        Console.WriteLine($"[URI ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($" - Path     : {context.Request.Path}");
        Console.WriteLine($" - Method   : {context.Request.Method}");
        Console.WriteLine($" - Host     : {context.Request.Host}");
        Console.WriteLine($" - Query    : {context.Request.QueryString}");

        Console.WriteLine(" - Headers  :");
        foreach (var header in context.Request.Headers)
        {
            Console.WriteLine($"    {header.Key}: {header.Value}");
        }

        Console.WriteLine($" - Message  : {ex.Message}");
        Console.WriteLine("======================================");

        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Bad Request: Invalid URI");
    }
    catch (Exception ex)
    {
        Console.WriteLine("======================================");
        Console.WriteLine($"[UNHANDLED ERROR] {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        Console.WriteLine($" - Path     : {context.Request.Path}");
        Console.WriteLine($" - Method   : {context.Request.Method}");
        Console.WriteLine($" - Host     : {context.Request.Host}");
        Console.WriteLine($" - Query    : {context.Request.QueryString}");

        Console.WriteLine(" - Headers  :");
        foreach (var header in context.Request.Headers)
        {
            Console.WriteLine($"    {header.Key}: {header.Value}");
        }

        Console.WriteLine($" - Exception: {ex}");
        Console.WriteLine("======================================");

        context.Response.StatusCode = 500;
        await context.Response.WriteAsync("Erreur interne du serveur.");
    }
});
*/
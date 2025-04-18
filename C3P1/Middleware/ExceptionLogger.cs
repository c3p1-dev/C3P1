using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace C3P1.Middleware
{
    public class ExceptionLogger
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionLogger> _logger;

        public ExceptionLogger(RequestDelegate next, ILogger<ExceptionLogger> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (UriFormatException ex)
            {
                LogRequest(context, ex, "URI ERROR");
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("Bad Request: Invalid URI");
            }
            catch (Exception ex)
            {
                LogRequest(context, ex, "UNHANDLED ERROR");
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("Erreur interne du serveur.");
            }
        }

        private void LogRequest(HttpContext context, Exception ex, string title)
        {
            _logger.LogError("======================================");
            _logger.LogError("[{Title}] {Timestamp}", title, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            _logger.LogError(" - Path     : {Path}", context.Request.Path);
            _logger.LogError(" - Method   : {Method}", context.Request.Method);
            _logger.LogError(" - Host     : {Host}", context.Request.Host);
            _logger.LogError(" - Query    : {Query}", context.Request.QueryString);

            _logger.LogError(" - Headers  :");
            foreach (var header in context.Request.Headers)
            {
                _logger.LogError("    {Key}: {Value}", header.Key, header.Value);
            }

            if (title == "URI ERROR")
            {
                _logger.LogError(" - Message  : {Message}", ex.Message);
            }
            else
            {
                _logger.LogError(" - Exception: {Exception}", ex.ToString());
            }

            _logger.LogError("======================================");
        }
    }
}

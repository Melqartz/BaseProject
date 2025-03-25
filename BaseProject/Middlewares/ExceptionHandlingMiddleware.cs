using System.Net;
using Serilog;

namespace BaseProject.Middlewares;

public class ExceptionHandlingMiddleware {
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            // Extract contextual details
            var controllerName = context.GetRouteValue("controller")?.ToString() ?? "Unknown";
            var actionName = context.GetRouteValue("action")?.ToString() ?? "Unknown";

            // Log the error with context
            Log.Error(ex, "An unhandled exception occurred in {Controller}/{Action}.", controllerName, actionName);

            // Return a generic error response// Check if the request is an AJAX call
            var isAjaxRequest = context.Request.Headers["X-Requested-With"] == "XMLHttpRequest";

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            if (isAjaxRequest)
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(
                    System.Text.Json.JsonSerializer.Serialize(new
                    {
                        success = false,
                        message = "An unexpected error occurred. Please contact support.",
                        error = ex.Message // Optional: Remove in production to avoid leaking details
                    })
                );
            }
            else
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("An unexpected error occurred. Please try again later.");
            }
        }
    }
}
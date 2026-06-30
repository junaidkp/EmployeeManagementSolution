using EmployeeManagement.Application.Common;
using System.Net;
using System.Text.Json;

namespace EmployeeManagement.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = GetStatusCode(exception);
            context.Response.StatusCode = statusCode;

            var response = ApiResponse<string>.ErrorResponse(GetMessage(exception));

            var json = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(json);
        }

        private static int GetStatusCode(Exception exception)
        {
            return exception switch
            {
                KeyNotFoundException => (int)HttpStatusCode.NotFound,          // 404
                ArgumentException => (int)HttpStatusCode.BadRequest,           // 400
                UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized, // 401
                InvalidOperationException => (int)HttpStatusCode.Conflict,     // 409
                _ => (int)HttpStatusCode.InternalServerError                   // 500
            };
        }

        private static string GetMessage(Exception exception)
        {
            return exception switch
            {
                KeyNotFoundException => exception.Message,
                ArgumentException => exception.Message,
                UnauthorizedAccessException => "Unauthorized access",
                InvalidOperationException => exception.Message,
                _ => "An unexpected error occurred. Please try again later."
            };
        }
    }
}

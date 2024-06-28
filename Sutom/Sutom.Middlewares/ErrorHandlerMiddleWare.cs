using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text.Json;

namespace Sutom.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            var errorDetails = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "An error occurred while processing your request.",
                ErrorType = exception.GetType().Name,
                StackTrace = exception.StackTrace
            };
            var jsonResponse = JsonSerializer.Serialize(errorDetails);
            return context.Response.WriteAsync(jsonResponse);
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public string? ErrorType { get; set; }
        public string? StackTrace { get; set; }
    }
}


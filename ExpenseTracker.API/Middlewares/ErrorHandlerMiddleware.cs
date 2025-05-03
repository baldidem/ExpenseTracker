using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace ExpenseTracker.API.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ErrorHandlerMiddleware(RequestDelegate next)
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
                await HandleExceptionAsync(context, ex);
            }
        }
        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            switch (exception)
            {
                case ArgumentNullException:
                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    break;
                case InvalidOperationException:
                    code = HttpStatusCode.Conflict;
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case KeyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
            }
            var result = JsonSerializer.Serialize(new
            {
                Status = (int)code,
                Message = exception.Message,
                Errors = exception.InnerException != null ? new[] { exception.InnerException.Message } : null,
                Path = context.Request.Path,
                TraceId = context.TraceIdentifier
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

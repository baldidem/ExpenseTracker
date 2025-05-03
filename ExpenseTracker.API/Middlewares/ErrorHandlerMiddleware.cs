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
                    code = HttpStatusCode.BadRequest; //400
                    break;
                case InvalidOperationException:
                    code = HttpStatusCode.Conflict; //409
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized; //401
                    break;
                case KeyNotFoundException:
                    code = HttpStatusCode.NotFound; // 404
                    break;
            }

            var result = JsonSerializer.Serialize(new
            {
                status = (int)code,
                message = exception.Message,
                errors = exception.InnerException != null ? new[] { exception.InnerException.Message } : null,
                path = context.Request.Path,
                traceId = context.TraceIdentifier
            });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}

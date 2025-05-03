namespace ExpenseTracker.API.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ErrorHandlerMiddleware>();
        }
    }
}

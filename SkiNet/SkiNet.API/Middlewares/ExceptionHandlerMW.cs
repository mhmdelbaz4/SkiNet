
namespace API.Middlewares
{
    public class ExceptionHandlerMW(IConfiguration configuration) : IMiddleware
    {
        public Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                return next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                context.Response.ContentType = "application/json";
                var response = new ExceptionResponse((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace);
                return context.Response.WriteAsJsonAsync(response);
            }
        }
    }

    public static class ExceptionHandlingMWExtension
    {
        public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMW>();
            return app;
        }
    }

}

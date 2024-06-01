using DDOSGuardService.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DDOSGuardService.Startup.Middleware
{
    public class HttpExceptionMiddleware : IMiddleware
    {

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (HttpException exception)
            {
                context.Response.StatusCode = (int)exception.StatusCode;
                await context.Response.WriteAsync(exception.Message);
            }
        }
    }
}
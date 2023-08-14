using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Http;

namespace Auth.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
       // private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlingMiddleware> _logger;
        public ErrorHandlingMiddleware( ILogger<ErrorHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async  Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,ex.Message);
                context.Response.StatusCode = 500;
              await  context.Response.WriteAsync($"Problem is: {ex.Message}");
            }
        }
    }

}



     
            
     



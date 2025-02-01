using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace UserDemo.Middleware
{
    public class ErrorHandlerMiddleware(RequestDelegate _next, ILogger<ErrorHandlerMiddleware> logger)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                logger.LogError(error, error.Message);
                var response = new
                {
                    error = new
                    {
                        message = "An error occurred while processing your request.",
                        details = error.Message
                    }
                };
                
                await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}

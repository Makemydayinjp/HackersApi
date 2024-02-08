using Microsoft.AspNetCore.Http;
using Santander.DeveloperTestAPI.Exceptions;
using System.Net;
using System.Text;

namespace Santander.DeveloperTestAPI.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(CouldNotFetchNewsException cnfnex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                var bytes = Encoding.UTF8.GetBytes(cnfnex.Message);
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
            catch (Exception)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var bytes = Encoding.UTF8.GetBytes("There was in internal server error.");
                await context.Response.Body.WriteAsync(bytes, 0, bytes.Length);
            }
        }
    }
}

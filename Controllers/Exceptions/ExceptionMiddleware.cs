using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WorkTracker.Controllers.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate)
        {
            _requestDelegate = requestDelegate;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception e)
            {
                await HandleError(httpContext, e);
            }
        }

        private static async Task HandleError(HttpContext httpContext, Exception e)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiException
            {
                Message = "Something went wrong",
                StatusCode = httpContext.Response.StatusCode
            };
            await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}

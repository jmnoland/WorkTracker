using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace WorkTracker.Controllers.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly RequestDelegate _requestDelegate;

        public ExceptionMiddleware(RequestDelegate requestDelegate,
                                   ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
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
                var _logger = _loggerFactory.CreateLogger<ExceptionMiddleware>();
                if (httpContext.Request.Path.HasValue)
                {
                    _logger.LogError($"{httpContext.Request.Path.Value} threw an error ${e.Message}");
                }
                _logger.LogError(e.StackTrace);
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

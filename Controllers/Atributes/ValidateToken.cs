using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Text;

namespace WorkTracker.Controllers.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateTokenAttribute : ActionFilterAttribute
    {
        private string _permission;
        private string _JwtSecret;
        private static IConfigurationRoot Configuration =>
            new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
        public ValidateTokenAttribute(string permission = null)
        {
            _permission = permission;
            _JwtSecret = Configuration.GetSection("AppSettings").GetSection("JwtSecret").Value;
        }

        public override void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var context = actionContext.HttpContext;
            if (!IsValidRequest(context.Request))
            {
                actionContext.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
            }

            base.OnActionExecuting(actionContext);
        }

        private bool IsValidRequest(HttpRequest request)
        {
            var requestUrl = RequestRawUrl(request);
            var parameters = ToDictionary(request.Form);
            var token = request.Headers["X-User-Token"];
            var isValid = true;
            if (_permission != null) isValid = PermissionAllowed(token, _permission);
            return (isValid && ValidateCurrentToken(token));
        }

        public bool PermissionAllowed(string token, string permission)
        {
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
            return decodedToken.Claims.Any(w => w.Type == "UserRole" && w.Value == permission);
        }

        public bool ValidateCurrentToken(string token)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_JwtSecret));

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        private static string RequestRawUrl(HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}{request.Path}{request.QueryString}";
        }

        private static IDictionary<string, string> ToDictionary(IFormCollection collection)
        {
            return collection.Keys
                .Select(key => new { Key = key, Value = collection[key] })
                .ToDictionary(p => p.Key, p => p.Value.ToString());
        }
    }
}

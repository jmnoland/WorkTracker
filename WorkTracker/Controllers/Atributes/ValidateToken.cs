using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net;
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
                actionContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return;
            }

            base.OnActionExecuting(actionContext);
        }

        private bool IsValidRequest(HttpRequest request)
        {
            var token = request.Cookies["X-User-Token"];
            if (token == null) token = request.Headers["X-User-Token"];
            if (token == null) return false;
            var isValid = true;
            if (_permission != null) isValid = PermissionAllowed(token, _permission);
            return (isValid && ValidateCurrentToken(token));
        }

        public bool PermissionAllowed(string token, string permission)
        {
            if (string.IsNullOrEmpty(token)) return false;
            var handler = new JwtSecurityTokenHandler();
            if (!handler.CanReadToken(token)) return false;
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
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = securityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}

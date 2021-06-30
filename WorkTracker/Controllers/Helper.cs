using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WorkTracker.Controllers
{
    public class Helper
    {
        public static int? GetRequestUserId(HttpContext request)
        {
            var token = request.Request.Cookies["X-User-Token"];
            if (token == null) token = request.Request.Headers["X-User-Token"];
            if (token == null) return null;
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
            var temp = decodedToken.Claims
                .Where(w => w.Type == "nameid")
                .Select(s => s.Value)
                .SingleOrDefault();
            if (temp != null) return int.Parse(temp);
            return null;
        }
    }
}

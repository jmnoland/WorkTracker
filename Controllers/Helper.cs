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
        public static int GetRequestUserId(HttpContext request)
        {
            var token = request.Request.Cookies["X-User-Token"];
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
            var temp = decodedToken.Claims
                .Where(w => w.Type == ClaimTypes.NameIdentifier)
                .Select(s => s.Value)
                .FirstOrDefault();
            return 0;
        }
    }
}

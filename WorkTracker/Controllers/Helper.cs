﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace WorkTracker.Controllers
{
    public static class Helper
    {
        public static int? GetRequestUserId(HttpContext request)
        {
            var token = request.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .LastOrDefault();
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
        
        public static int? GetRequestTeamIds(HttpContext request)
        {
            var token = request.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .LastOrDefault();
            if (token == null) return null;
            var handler = new JwtSecurityTokenHandler();
            var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
            var temp = decodedToken.Claims
                .Where(w => w.Type == "teamid")
                .Select(s => s.Value)
                .SingleOrDefault();
            if (temp != null) return int.Parse(temp);
            return null;
        }
    }
}

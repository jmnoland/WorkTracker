using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace WorkTracker.Services
{
    public static class Helper
    {
		public static string GenerateToken(int userId, int? teamId, string[] permissions, string secret)
		{
			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
				new Claim("teamId", teamId.ToString())
			};
			foreach (var permission in permissions)
			{
				claims.Add(new Claim(ClaimTypes.Role, permission));
			}

			var tokenHandler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddMinutes(30),
				SigningCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}
	}
}

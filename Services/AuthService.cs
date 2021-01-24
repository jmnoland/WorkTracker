using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkTracker.Models;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Services
{
    public class AuthService : IAuthService
    {
		public static readonly string ClaimsRole = "UserRole";
		private readonly IRoleRepository _roleRepository;
		private readonly IOptions<AppSettings> _appSettings;
		public AuthService(IRoleRepository roleRepository,
						   IOptions<AppSettings> appSettings)
        {
			_roleRepository = roleRepository;
			_appSettings = appSettings;
        }

		public string GenerateToken(int userId)
        {
			var secret = _appSettings.Value.JwtSecret;
			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

			var role = _roleRepository.GetUserRole(userId);
			var permissions = role.Permissions.Split(',');
			var claims = new List<Claim>()
			{
				new Claim(ClaimTypes.NameIdentifier, userId.ToString())
			};
			foreach(var permission in permissions)
            {
				claims.Add(new Claim(ClaimsRole, permission));
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

		public bool ValidateCurrentToken(string token)
		{
			var secret = _appSettings.Value.JwtSecret;
			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

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
	}
}

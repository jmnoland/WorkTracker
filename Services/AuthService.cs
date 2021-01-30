using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using WorkTracker.Models;
using WorkTracker.Models.Requests;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services.Interfaces;

namespace WorkTracker.Services
{
    public class AuthService : IAuthService
    {
		public static readonly string ClaimsRole = "UserRole";
		private readonly IRoleRepository _roleRepository;
		private readonly IUserRepository _userRepository;
		private readonly IOptions<AppSettings> _appSettings;
		public AuthService(IRoleRepository roleRepository,
						   IUserRepository userRepository,
						   IOptions<AppSettings> appSettings)
        {
			_roleRepository = roleRepository;
			_userRepository = userRepository;
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

		public string RefreshToken(string token)
        {
			var handler = new JwtSecurityTokenHandler();
			var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
			var userId = decodedToken.Claims
				.Where(w => w.Type == ClaimTypes.NameIdentifier)
				.Select(s => s.Value).FirstOrDefault();
			return GenerateToken(int.Parse(userId));
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

		public bool PermissionAllowed(string token, string permission)
        {
			var handler = new JwtSecurityTokenHandler();
			var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
			return decodedToken.Claims.Any(w => w.Type == ClaimsRole && w.Value == permission);
		}

		public string Login(UserLoginRequest request)
        {
			var user = _userRepository.Find(w => w.Email == request.Email).FirstOrDefault();
			
			if (user != null && user.Password == request.Password)
            {
				return GenerateToken(user.UserId);
            }
			return null;
        }
	}
}

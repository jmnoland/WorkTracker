using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WorkTracker.Repositories.Interfaces;

namespace WorkTracker.Services
{
    public class AuthService : IAuthService
    {
		public static readonly string ClaimsRole = "UserRole";
		private readonly IRoleRepository _roleRepository;
		public AuthService(IRoleRepository roleRepository)
        {
			_roleRepository = roleRepository;
        }

		public string GenerateToken(int userId)
        {
			var secret = "asdv234234^&%&^%&^hjsdfb2%%%";
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
    }
}

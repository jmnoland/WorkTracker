using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkTracker.Models;
using WorkTracker.Models.Requests;
using WorkTracker.Repositories.Interfaces;
using WorkTracker.Services.Interfaces;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Runtime.CompilerServices;

namespace WorkTracker.Services
{
    public class AuthService : IAuthService
    {
		public static readonly string ClaimsRole = "UserRole";
		private readonly IRoleRepository _roleRepository;
		private readonly IUserRepository _userRepository;
        private readonly ITeamRepository _teamRepository;
		private readonly IOptions<AppSettings> _appSettings;
        private readonly RandomNumberGenerator _rng;
        public AuthService(IRoleRepository roleRepository,
						   IUserRepository userRepository,
                           ITeamRepository teamRepository,
						   IOptions<AppSettings> appSettings)
        {
			_roleRepository = roleRepository;
			_userRepository = userRepository;
            _teamRepository = teamRepository;
			_appSettings = appSettings;
            _rng = RandomNumberGenerator.Create();
        }

        public async Task<string> GenerateToken(int userId)
        {
			var secret = _appSettings.Value.JwtSecret;
			var securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));

			var role = await _roleRepository.GetUserRole(userId);
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

		public async Task<string> RefreshToken(string token)
        {
			var handler = new JwtSecurityTokenHandler();
			var decodedToken = handler.ReadToken(token) as JwtSecurityToken;
			var userId = decodedToken.Claims
				.Where(w => w.Type == ClaimTypes.NameIdentifier)
				.Select(s => s.Value).FirstOrDefault();
			return await GenerateToken(int.Parse(userId));
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

		public async Task<string> Login(UserLoginRequest request)
        {
			var user = _userRepository.Find(w => w.Email == request.Email).FirstOrDefault();
			
			if (user != null && VerifyHashedPassword(user.Password, request.Password))
            {
				return await GenerateToken(user.UserId);
            }
			return null;
        }

        // Register for user without exisiting team/organisation or invite
        public async Task Register(UserRegisterRequest request)
        {
            var user = new Models.DataModels.User
            {
                Email = request.Email,
                Name = request.Name,
                Password = HashPassword(request.Password, _rng, KeyDerivationPrf.HMACSHA256),
                RoleId = request.RoleId ?? 1,
                UserId = 0,
            };
            await _userRepository.Add(user);
            var team = new Models.DataModels.Team
            {
                Name = Guid.NewGuid().ToString(),
                OrganisationId = null,
                TeamId = 0,
            };
            await _teamRepository.Add(team);
            await _teamRepository.AssignUser(user.UserId, team.TeamId);
        }

        public async Task<string> DemoLogin()
        {
            var user = _userRepository.Find(w => w.Email == "demo@email.com").FirstOrDefault();

            if (user != null)
            {
                return await GenerateToken(user.UserId);
            }
            return null;
        }

        #region Private Methods
        // https://github.com/aspnet/Identity/blob/c7276ce2f76312ddd7fccad6e399da96b9f6fae1/src/Core/PasswordHasher.cs
        // HashPasswordV3
        private static string HashPassword(string password, RandomNumberGenerator rng, KeyDerivationPrf prf)
        {
            int iterCount = 10000;
            int saltSize = 128 / 8;
            int numBytesRequested = 256 / 8;

            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);
            byte[] subkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, numBytesRequested);

            var outputBytes = new byte[13 + salt.Length + subkey.Length];
            outputBytes[0] = 0x01; // format marker
            WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
            WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
            WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
            Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
            Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
            return Convert.ToBase64String(outputBytes);
        }
        private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
        {
            return ((uint)(buffer[offset + 0]) << 24)
                | ((uint)(buffer[offset + 1]) << 16)
                | ((uint)(buffer[offset + 2]) << 8)
                | ((uint)(buffer[offset + 3]));
        }
        private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
        {
            buffer[offset + 0] = (byte)(value >> 24);
            buffer[offset + 1] = (byte)(value >> 16);
            buffer[offset + 2] = (byte)(value >> 8);
            buffer[offset + 3] = (byte)(value >> 0);
        }
        private static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] decodedHashedPassword = Convert.FromBase64String(hashedPassword);
            int iterCount = default(int);
            try
            {
                // Read header information
                KeyDerivationPrf prf = (KeyDerivationPrf)ReadNetworkByteOrder(decodedHashedPassword, 1);
                iterCount = (int)ReadNetworkByteOrder(decodedHashedPassword, 5);
                int saltLength = (int)ReadNetworkByteOrder(decodedHashedPassword, 9);

                // Read the salt: must be >= 128 bits
                if (saltLength < 128 / 8)
                {
                    return false;
                }
                byte[] salt = new byte[saltLength];
                Buffer.BlockCopy(decodedHashedPassword, 13, salt, 0, salt.Length);

                // Read the subkey (the rest of the payload): must be >= 128 bits
                int subkeyLength = decodedHashedPassword.Length - 13 - salt.Length;
                if (subkeyLength < 128 / 8)
                {
                    return false;
                }
                byte[] expectedSubkey = new byte[subkeyLength];
                Buffer.BlockCopy(decodedHashedPassword, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

                // Hash the incoming password and verify it
                byte[] actualSubkey = KeyDerivation.Pbkdf2(password, salt, prf, iterCount, subkeyLength);
                return ByteArraysEqual(actualSubkey, expectedSubkey);
            }
            catch
            {
                return false;
            }
        }
        // Compares two byte arrays for equality. The method is specifically written so that the loop is not optimized.
        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        private static bool ByteArraysEqual(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            if (a == null || b == null || a.Length != b.Length)
            {
                return false;
            }
            var areSame = true;
            for (var i = 0; i < a.Length; i++)
            {
                areSame &= (a[i] == b[i]);
            }
            return areSame;
        }
        #endregion
    }
}

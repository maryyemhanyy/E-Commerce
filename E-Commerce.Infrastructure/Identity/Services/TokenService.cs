using E_Commerce.Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Infrastructure.Identity.Services
{
    public class TokenService(IOptions<JWTSettings> options) : ITokenService
    {
        private readonly JWTSettings _jwtSettings = options.Value;
        public string CreateToken(string userId, string email, string username, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, userId),
                new (ClaimTypes.Email , email),
                new (ClaimTypes.Name ,username)
            };
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            if (string.IsNullOrEmpty(_jwtSettings.SecretKey)) throw new InvalidOperationException("JWT SecretKey is missing");
            if (_jwtSettings.SecretKey.Length < 32) throw new InvalidOperationException("JWT SecretKey is too short");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes),
                signingCredentials: credentials
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class JWTSettings
    {
        public string SecretKey { get; init; } = default!;
        public string Issuer { get; init; } = default!;
        public string Audience { get; init; } = default!;
        public int ExpirationMinutes { get; init; } = 60;

    }
}

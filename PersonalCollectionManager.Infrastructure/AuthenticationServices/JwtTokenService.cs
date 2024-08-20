using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PersonalCollectionManager.Application.Interfaces.IAuthService;
using PersonalCollectionManager.Domain.Entities;


namespace PersonalCollectionManager.Infrastructure.AuthenticationServices
{
    public class JwtTokenService : IJwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomNumber);
            }
            var uniqueToken = $"{Convert.ToBase64String(randomNumber)}|{Guid.NewGuid()}|{DateTime.UtcNow.Ticks}";

            using (var sha256 = SHA256.Create())
            {
                var hashedToken = sha256.ComputeHash(Encoding.UTF8.GetBytes(uniqueToken));
                return Convert.ToBase64String(hashedToken);
            }
        }

        public string GenerateToken(User user)
        {
            var keyString = _configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("Jwt:Key not found in appsettings.json");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin ? "Admin" : "User"),
                new Claim("isBlocked", user.IsBlocked.ToString()),
                new Claim("PreferredLanguage", user.PreferredLanguage),
                new Claim("PreferredThemeDark", user.PreferredThemeDark ? "true" : "false")
            };

            var expiryTimeConfig = _configuration["JwtSettings:ExpiryTimeSeconds"];

            if (!int.TryParse(expiryTimeConfig, out int expiryInSecond))
            {
                expiryInSecond = 3600;
            }

            var token = new JwtSecurityToken(
                _configuration["JwtSettings:Issuer"],
                _configuration["JwtSettings:Audience"],
                claims,
                expires: DateTime.UtcNow.AddSeconds(expiryInSecond),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var keyString = _configuration["JwtSettings:Key"] ?? throw new InvalidOperationException("Jwt:Key not found in appsettings.json");

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["JwtSettings:Issuer"],
                ValidAudience = _configuration["JwtSettings:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(keyString))
            };

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            var jwtSecurityToken = validatedToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }
    }

}

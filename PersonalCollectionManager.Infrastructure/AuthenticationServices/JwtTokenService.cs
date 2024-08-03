using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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
            
        }

        public string GenerateToken(User user)
        {
            var keyString = _configuration["Jwt:Key"]?? throw new InvalidOperationException("Jwt:Key not found in appsettings.json");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.IsAdmin? "Admin" : "User"),
                new Claim("isBlocked", user.IsBlocked.ToString()),
                new Claim("PrefrredLanguage", user.PrefrredLanguage),
                new Claim("PreffrredThemeDark", user.PreffrredThemeDark? "true" : "false")
            };

            if(!int.TryParse(_configuration["Jwt:ExpiryInMinutes"], out int expiryInMinutes))
            {
                expiryInMinutes = 30;
            }

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(expiryInMinutes),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Helpers
{
    public static class JwtHelper
    {
        private static readonly string SecretKey = Environment.GetEnvironmentVariable("JWT_SECRET_KEY");

        public static string GenerateToken(User user)
        {
            if (string.IsNullOrEmpty(SecretKey))
            {
                throw new InvalidOperationException("JWT_SECRET_KEY is not set in the environment variables");
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.RoleId.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "NEEM Backend",
                audience: "NEEM Frontend",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                    SecurityAlgorithms.HmacSha256)
            );

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
        public static bool ValidateToken(string token, out ClaimsPrincipal principal)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = "NEEM Backend",
                    ValidateAudience = true,
                    ValidAudience = "NEEM Frontend",
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey)),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };

                    principal = tokenHandler.ValidateToken(token, validationParameters, out _);
                    return true;
            }
            catch
            {
                    
                principal = null;
                return false;
            }
        }
    }
}
using Common.ServiceInterfaces;
using EntityData;
using EntityData.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace AppOutSideAPI.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        public JwtService(IConfiguration configuration)
        {
            _config = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("role", user.Role),		// must "role" or ClaimTypes.Role
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddMinutes(double.Parse(_config["Jwt:Expire"]));
            var jwt = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: exp,
                signingCredentials: creds);

            return CreateToken(new JwtSecurityTokenHandler().WriteToken(jwt), (long)exp.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        Token CreateToken(string token, long exp) => new Token { AccessToken = token, Expire = exp };
    }
}

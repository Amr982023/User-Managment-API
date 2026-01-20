using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTOs;
using Application_Layer.Interfaces.Security;
using Application_Layer.Options;
using Domain_Layer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure_Layer.Repositories.JWT
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly IConfiguration _configuration;
        public TokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        string GetRole(short role)
        {
            return role switch
            {
                1 => "User",
                2 => "Admin"    
            };
        }

        public string GenerateToken(User User)
        {
            var TokenHandler = new JwtSecurityTokenHandler();
            var options = _configuration.GetSection("Jwt").Get<JwtOptions>();

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = options.Issuer,
                Audience = options.Audience,
                Expires = DateTime.UtcNow.AddMinutes(options.Lifetime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey)), SecurityAlgorithms.HmacSha256Signature),
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new(ClaimTypes.NameIdentifier , User.Id.ToString()),
                    new(ClaimTypes.Name , User.Username),
                    new(ClaimTypes.Role , GetRole((short)User.UserRole)),                  
                })
            };

            var Token = TokenHandler.CreateToken(TokenDescriptor);
            var jwt = TokenHandler.WriteToken(Token);
            return jwt;
        }

    }
}

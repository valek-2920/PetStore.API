using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Pet_Store.Application.Configurations;
using Pet_Store.Application.Contracts.Managers;
using Pet_Store.Domain.Models.PlainModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Pet_Store.Application.Managers
{
    public class JwtManager : IJwtManager
    {
        public JwtManager(IOptions<JwtConfiguration> options)
        {
            _configuration = options.Value;
        }

        readonly JwtConfiguration _configuration;

        public JwtToken Authenticate(JwtUser input)
        {
            var handler = new JwtSecurityTokenHandler();
            var secretKey = Encoding.UTF8.GetBytes(_configuration.SigninKey);
            var descriptor =
                new SecurityTokenDescriptor
                {
                    Subject =
                        new ClaimsIdentity
                            (new[] { new Claim(ClaimTypes.Name, input.UserName) }),
                    Expires = DateTime.UtcNow.AddMinutes(_configuration.LifeTime),
                    SigningCredentials =
                        new SigningCredentials
                            (new SymmetricSecurityKey(secretKey), SecurityAlgorithms.HmacSha256Signature)
                };

            var token = handler.CreateToken(descriptor);
            return new JwtToken { Token = handler.WriteToken(token) };
        }

        public bool IsTokenValid(string token, out SecurityToken authorizedToken)
        {
            authorizedToken = null;

            var secretKey = Encoding.UTF8.GetBytes(_configuration.SigninKey);
            var handler = new JwtSecurityTokenHandler();

            try
            {
                handler.ValidateToken
                    (
                        token,
                        new TokenValidationParameters
                        {
                            ValidateIssuer = false,
                            ValidateAudience = false,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = _configuration.Issuer,
                            ValidAudience = _configuration.Audience,
                            IssuerSigningKey = new SymmetricSecurityKey(secretKey)
                        },
                        out SecurityToken validatedToken
                    );

                authorizedToken = validatedToken;
                if (authorizedToken.ValidTo < DateTime.UtcNow)
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}

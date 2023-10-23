using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.CQRS.Identity.Users.Commands.SignInUser;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NexTube.Persistence.Identity {
    public class JwtService : IJwtService {
        private readonly IConfiguration _configuration;
        private readonly IDateTimeService _dateTimeService;

        public JwtService(IConfiguration configuration, IDateTimeService dateTimeService) {
            _configuration = configuration;
            _dateTimeService = dateTimeService;
        }

        public string GenerateToken(int userId, UserLookup user) {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetValue<string>("Jwt:Key")));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>();
            foreach (var role in user.Roles) {
                claims.Add(new Claim(ClaimTypes.Role, role));
                claims.Add(new Claim("roles", role));
            }
            claims.Add(new Claim("user_id", userId.ToString()));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("email", user.Email));

            var token = new JwtSecurityToken(
                _configuration.GetValue<string>("Jwt:Issuer"),
                _configuration.GetValue<string>("Jwt:Audience"),
                claims,
                expires: _dateTimeService.Now.AddHours(_configuration.GetValue<int>("Jwt:ExpiresAfterHours")),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

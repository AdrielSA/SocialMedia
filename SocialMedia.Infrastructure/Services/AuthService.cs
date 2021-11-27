using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Options;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Infrastructure.Services
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordService _passwordService;
        private readonly ISecurityService _securityServices;
        private readonly AuthOptions _authOptions;

        public AuthService(
            ISecurityService securityService,
            IPasswordService passwordService,
            IOptions<AuthOptions> authOptions)
        {
            _securityServices = securityService;
            _passwordService = passwordService;
            _authOptions = authOptions.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<(bool, Security)> IsValidUser(UserLogin login)
        {
            var user = await _securityServices.GetLogin(login);
            var isvalid = _passwordService.Check(user.Password, login.Password);
            return (isvalid, user);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="security"></param>
        /// <returns></returns>
        public string GenerateToken(Security security)
        {
            //Header
            var symetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.SecretKey));
            var signingCredentials = new SigningCredentials(symetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(signingCredentials);

            //Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, security.UserName),
                new Claim("User", security.User),
                new Claim(ClaimTypes.Role, security.Role.ToString())
            };

            //Payload
            var payload = new JwtPayload
            (
                _authOptions.Issuer,
                _authOptions.Audience,
                claims,
                DateTime.Now,
                DateTime.UtcNow.AddMinutes(_authOptions.ValidTime)
            );

            var token = new JwtSecurityToken(header, payload);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

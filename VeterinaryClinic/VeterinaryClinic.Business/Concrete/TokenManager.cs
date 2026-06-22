using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using VeterinaryClinic.Business.Abstract;
using VeterinaryClinic.Business.Configuration;
using VeterinaryClinic.Entities.Concrete;

namespace VeterinaryClinic.Business.Concrete
{

    public class TokenManager : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<TokenManager> _logger;

        public TokenManager(IOptions<JwtSettings> jwtSettingsOptions, UserManager<User> userManager, ILogger<TokenManager> logger)
        {
            _jwtSettings = jwtSettingsOptions.Value;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<string> CreateToken(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var payload = new JwtPayload(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: null,
                notBefore: null,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes)
            );

            payload["sub"] = user.Id.ToString();
            payload["name"] = user.UserName;
            payload["email"] = user.Email;

            if (userRoles.Count == 1)
                payload["role"] = userRoles[0];
            else if (userRoles.Count > 1)
                payload["role"] = userRoles;

            var header = new JwtHeader(creds);
            var token = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

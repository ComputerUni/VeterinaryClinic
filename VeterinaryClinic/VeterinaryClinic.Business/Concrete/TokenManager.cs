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

            // 1. Tüm claimleri tek bir listede topla
            var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
        new Claim(ClaimTypes.Name, user.UserName),
        new Claim(ClaimTypes.Email, user.Email)
    };

            // 2. Rolleri ekle (Doğrudan "role" anahtarıyla)
            foreach (var role in userRoles)
            {
                claims.Add(new Claim("role", role));
            }

            // 3. Anahtarı ve kimlik bilgilerini oluştur
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // 4. TokenDescriptor'ı oluştur
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims), // <-- claims listesi burada paketleniyor!
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}

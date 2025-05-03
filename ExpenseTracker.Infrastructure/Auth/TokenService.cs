using ExpenseTracker.Application.Interfaces.Auth;
using ExpenseTracker.Application.Settings;
using ExpenseTracker.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ExpenseTracker.Infrastructure.Auth
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;

        public TokenService(JwtSettings jwtSettings)
        {
            _jwtSettings = jwtSettings;
        }

        public string GenerateToken(User user) // jwtSettings'e gore token uretecek method.
        {
            //Kullanici bilgileriyle claimleri olusturuyoruz.
            var claims = new List<Claim>
            {
                //new Claim("UserId", user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim("FistName" , user.Name),
                new Claim("Surname", user.Surname),
                new Claim("UserName",user.Surname),
                new Claim("Email",user.Email),
                new Claim("RoleId",user.RoleId.ToString()),
                new Claim(ClaimTypes.Role,user.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpireMinutes),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}

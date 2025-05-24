using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Service.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Feilds

        private readonly JWT _jwtSettings;


        #endregion

        #region ctor
        public AuthenticationService(IOptions<JWT> jwtOptions)
        {
            _jwtSettings = jwtOptions.Value;
        }
        #endregion

        #region functions
        public async Task<string> GenerateJWTToken(ApplicationUser user)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(nameof(UserClaims.Id), user.Id.ToString()),
                new Claim(nameof(UserClaims.FirstName), user.FirstName??""),
                new Claim(nameof(UserClaims.LastName), user.LastName??""),
                new Claim(nameof(UserClaims.PhoneNumber), user.PhoneNumber ?? ""),
                new Claim(nameof(UserClaims.PhoneNumber), user.Email ?? "")
            };

            var Accesstoken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds
                );
            var token = new JwtSecurityTokenHandler().WriteToken(Accesstoken);
            return token;
        }
        #endregion
    }
}

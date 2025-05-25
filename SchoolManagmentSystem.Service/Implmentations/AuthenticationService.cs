using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using SchoolManagmentSystem.Service.Abstracts;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Feilds

        private readonly JWT _jwtSettings;
        private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken;
        private readonly IRefreshTokenRepository _refreshTokenRepository;


        #endregion

        #region ctor
        public AuthenticationService(IOptions<JWT> jwtOptions, IRefreshTokenRepository refreshTokenRepository)
        {
            _jwtSettings = jwtOptions.Value;
            _UserRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _refreshTokenRepository = refreshTokenRepository;
        }
        #endregion

        #region functions
        public async Task<JWTAuthResponse> GenerateJWTToken(ApplicationUser user)
        {
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
            var claims = GetUserClaims(user);

            var Accesstoken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(Accesstoken);


            var refreshToken = GetRefreshToken(user);
            var userRefreshToken = new UserRefreshToken
            {
                AddTime = DateTime.Now,
                ExpirationDate = refreshToken.ExpireAt,
                JwtId = Accesstoken.Id,
                RefreshToken = refreshToken.Refresh_Token,
                UserId = user.Id,
                Token = token,
                IsUsed = false,
                IsRevoked = false

            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            return new JWTAuthResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }

        public RefreshToken GetRefreshToken(ApplicationUser user)
        {
            var refToken = new RefreshToken
            {
                Refresh_Token = GenerateRefreshToken(),
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpirationInDays),
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName
            };
            _UserRefreshToken.AddOrUpdate(refToken.Refresh_Token, refToken, (r, s) => refToken);
            return refToken;
        }
        public string GenerateRefreshToken()
        {
            var rondomNumber = new byte[32];
            var rondomNumberGenerator = RandomNumberGenerator.Create();
            rondomNumberGenerator.GetBytes(rondomNumber);
            return Convert.ToBase64String(rondomNumber);
        }

        public List<Claim> GetUserClaims(ApplicationUser user)
        {
            var userclaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(nameof(UserClaims.Id), user.Id.ToString()),
                new Claim(nameof(UserClaims.FirstName), user.FirstName??""),
                new Claim(nameof(UserClaims.LastName), user.LastName??""),
                new Claim(nameof(UserClaims.PhoneNumber), user.PhoneNumber ?? ""),
                new Claim(nameof(UserClaims.Email), user.Email ?? "")
            };
            return userclaims;
        }
        #endregion
    }
}

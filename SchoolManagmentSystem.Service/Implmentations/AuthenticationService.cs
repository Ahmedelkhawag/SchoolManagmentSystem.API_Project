using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Data.Results;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using SchoolManagmentSystem.Service.Abstracts;
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
        // private readonly ConcurrentDictionary<string, RefreshToken> _UserRefreshToken;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UserManager<ApplicationUser> _userManager;


        #endregion  

        #region ctor
        public AuthenticationService(IOptions<JWT> jwtOptions, IRefreshTokenRepository refreshTokenRepository, UserManager<ApplicationUser> userManager)
        {
            _jwtSettings = jwtOptions.Value;
            // _UserRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
            _refreshTokenRepository = refreshTokenRepository;
            _userManager = userManager;
        }
        #endregion

        #region functions
        public async Task<JWTAuthResponse> GenerateJWTToken(ApplicationUser user)
        {


            var (Accesstoken, token) = await GetJWTToken(user);

            var refreshToken = GetRefreshToken(user);
            var userRefreshToken = new UserRefreshToken
            {
                AddTime = DateTime.Now,
                ExpirationDate = refreshToken.ExpireAt,
                JwtId = Accesstoken.Id,
                RefreshToken = refreshToken.Refresh_Token,
                UserId = user.Id,
                Token = token,
                IsUsed = true,
                IsRevoked = false

            };
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            return new JWTAuthResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken
            };
        }
        private async Task<(JwtSecurityToken, string)> GetJWTToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256Signature);
            var claims = await GetUserClaims(user/*, roles*/);

            var Accesstoken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
                signingCredentials: creds
                );

            var token = new JwtSecurityTokenHandler().WriteToken(Accesstoken);
            return (Accesstoken, token);
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
            // _UserRefreshToken.AddOrUpdate(refToken.Refresh_Token, refToken, (r, s) => refToken);
            return refToken;
        }
        public string GenerateRefreshToken()
        {
            var rondomNumber = new byte[32];
            var rondomNumberGenerator = RandomNumberGenerator.Create();
            rondomNumberGenerator.GetBytes(rondomNumber);
            return Convert.ToBase64String(rondomNumber);
        }

        public async Task<List<Claim>> GetUserClaims(ApplicationUser user/*, IList<string> roles*/)
        {
            var roles = await _userManager.GetRolesAsync(user); // Fetch roles asynchronously

            var userclaims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(nameof(UserClaims.FirstName), user.FirstName??""),
                new Claim(nameof(UserClaims.LastName), user.LastName??""),
                new Claim(nameof(UserClaims.PhoneNumber), user.PhoneNumber ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
            };

            if (roles != null && roles.Count > 0)
            {
                userclaims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
            }
            var userClaims = await _userManager.GetClaimsAsync(user); // Fetch user claims asynchronously
            if (userClaims != null && userClaims.Count > 0)
            {
                userclaims.AddRange(userClaims);
            }
            return userclaims;
        }

        public async Task<JWTAuthResponse> CreateRefreshToken(string accessToken, string refreshToken)
        {
            var jwtToken = ValidateAccessToken(accessToken);

            if (jwtToken.ValidTo > DateTime.UtcNow)
            {
                throw new ArgumentException("Access token is not expired.", nameof(accessToken));
            }

            var userId = GetUserIdFromToken(jwtToken);
            var userRefToken = await GetUserRefreshTokenAsync(accessToken, refreshToken, userId);

            ValidateRefreshToken(userRefToken);

            var user = await GetUserByIdAsync(userId);
            var newToken = await GetJWTToken(user);

            var newRefreshToken = CreateNewRefreshToken(user, refreshToken, userRefToken.ExpirationDate);

            return new JWTAuthResponse
            {
                AccessToken = newToken.Item2,
                RefreshToken = newRefreshToken
            };
        }

        private JwtSecurityToken ValidateAccessToken(string accessToken)
        {
            var jwtToken = ReadToken(accessToken);
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                throw new ArgumentException("Invalid access token.", nameof(accessToken));
            }
            return jwtToken;
        }

        private string GetUserIdFromToken(JwtSecurityToken jwtToken)
        {
            return jwtToken.Claims.FirstOrDefault(c => c.Type == nameof(UserClaims.Id))?.Value
                   ?? throw new ArgumentException("User ID not found in token.");
        }

        private async Task<UserRefreshToken> GetUserRefreshTokenAsync(string accessToken, string refreshToken, string userId)
        {
            var userRefToken = await _refreshTokenRepository
                .GetTableNoTracking()
                .FirstOrDefaultAsync(r => r.Token == accessToken &&
                                          r.RefreshToken == refreshToken &&
                                          r.UserId.ToString() == userId);

            if (userRefToken == null)
            {
                throw new ArgumentException("Refresh token not found or invalid.", nameof(refreshToken));
            }

            return userRefToken;
        }

        private void ValidateRefreshToken(UserRefreshToken userRefToken)
        {
            if (userRefToken.ExpirationDate < DateTime.UtcNow)
            {
                userRefToken.IsUsed = false;
                userRefToken.IsRevoked = true;
                _refreshTokenRepository.UpdateAsync(userRefToken).Wait();

                throw new ArgumentException("Refresh token is expired.", nameof(userRefToken.RefreshToken));
            }
        }

        private async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                throw new ArgumentException("User not found.", nameof(userId));
            }
            return user;
        }

        private RefreshToken CreateNewRefreshToken(ApplicationUser user, string refreshToken, DateTime expirationDate)
        {
            return new RefreshToken
            {
                UserId = user.Id.ToString(),
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                ExpireAt = expirationDate,
                Refresh_Token = refreshToken,
            };
        }


        private JwtSecurityToken ReadToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken), "Token cannot be null or empty.");
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(accessToken);

            return jwtToken;
        }

        public async Task<string> ValidateToken(string accessToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifetime,
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                ValidIssuer = _jwtSettings.Issuer,
                ValidAudience = _jwtSettings.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key))
            };
            try
            {
                var validationResult = tokenHandler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);
                if (validationResult is null)
                {
                    throw new SecurityTokenException("Invalid token.");

                }
                return "Token is valid.";


            }
            catch (Exception ex)
            {
                return $"Token validation failed: {ex.Message}";
            }
        }
        #endregion
    }
}

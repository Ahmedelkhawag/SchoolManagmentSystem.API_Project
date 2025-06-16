using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Helpers;
using System.Security.Claims;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JWTAuthResponse> GenerateJWTToken(ApplicationUser user);
        string GenerateRefreshToken();
        RefreshToken GetRefreshToken(ApplicationUser user);
        List<Claim> GetUserClaims(ApplicationUser user, IList<string> roles);
        Task<JWTAuthResponse> CreateRefreshToken(string accessToken, string refreshToken);
        Task<string> ValidateToken(string accessToken);

    }
}

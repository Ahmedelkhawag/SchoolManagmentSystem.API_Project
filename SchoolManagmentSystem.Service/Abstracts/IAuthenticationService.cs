﻿using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Data.Results;
using System.Security.Claims;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<JWTAuthResponse> GenerateJWTToken(ApplicationUser user);
        string GenerateRefreshToken();
        RefreshToken GetRefreshToken(ApplicationUser user);
        Task<List<Claim>> GetUserClaims(ApplicationUser user/*, IList<string> roles*/);
        Task<JWTAuthResponse> CreateRefreshToken(string accessToken, string refreshToken);
        Task<string> ValidateToken(string accessToken);
        Task<string> ConfirmEmailAsync(int userId, string code);
        Task<string> ForgotPasswordAsync(string email);
        Task<string> ResetPasswordAsync(ResetPasswordParams passwordParams);

    }
}

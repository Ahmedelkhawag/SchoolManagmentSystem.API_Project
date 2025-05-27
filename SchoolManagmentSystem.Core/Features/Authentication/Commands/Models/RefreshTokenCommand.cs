using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Data.Helpers;

namespace SchoolManagmentSystem.Core.Features.Authentication.Commands.Models
{
    public class RefreshTokenCommand : IRequest<GeneralResponse<JWTAuthResponse>>
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}

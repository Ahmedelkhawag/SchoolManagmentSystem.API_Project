using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IAuthenticationService
    {
        Task<string> GenerateJWTToken(ApplicationUser user);
    }
}

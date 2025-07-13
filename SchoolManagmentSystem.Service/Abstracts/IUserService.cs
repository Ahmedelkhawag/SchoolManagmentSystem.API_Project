using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IUserService
    {
        Task<string> RegisterUserAync(ApplicationUser user, string password);
    }
}

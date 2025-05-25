using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Abstract;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Interfaces
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {
    }
}

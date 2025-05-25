using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Implmentation;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Implementations
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private readonly ApplicationDbContext _context;
        private readonly DbSet<UserRefreshToken> _userRefreshToken;
        #endregion
        #region Ctors
        public RefreshTokenRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _userRefreshToken = context.Set<UserRefreshToken>();

        }
        #endregion
        #region Methods

        #endregion
    }
}

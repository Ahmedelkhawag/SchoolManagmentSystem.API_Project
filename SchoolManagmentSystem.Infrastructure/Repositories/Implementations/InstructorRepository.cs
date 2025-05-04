using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Implmentation;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Implementations
{
    public class InstructorRepository : GenericRepositoryAsync<Instructor>, IInstructorRepository
    {
        #region Fields & Properties
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Instructor> _dbSet;
        #endregion
        #region Constructors
        public InstructorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Instructor>();
        }
        #endregion
        #region Interface implmentations
        //public async Task<List<Instructor>> GetAllInstructorsAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Implmentation;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Implementations
{
    public class SubjectRepository : GenericRepositoryAsync<Subject>, ISubjectRepository
    {
        #region Fields & Properties
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Subject> _dbSet;
        #endregion
        #region Constructors
        public SubjectRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Subject>();
        }
        #endregion
        #region Interface implmentations
        //public async Task<List<Subject>> GetAllSubjectsAsync()
        //{ 
        //    return await _dbSet.ToListAsync();
        // }
        #endregion
    }
}

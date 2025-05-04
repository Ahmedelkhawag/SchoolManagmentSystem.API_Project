using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Implmentation;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Implementations
{
    public class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {

        #region Fields & Properties
        private readonly ApplicationDbContext _context;
        private readonly DbSet<Department> _dbSet;
        #endregion
        #region Constructors
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Department>();
        }
        #endregion
        #region Interface implmentations
        //public async Task<List<Department>> GetAllDepartmentsAsync()
        //{
        //    return await _dbSet.ToListAsync();
        //}
        #endregion
    }
}

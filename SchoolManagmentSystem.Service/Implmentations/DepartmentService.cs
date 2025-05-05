using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class DepartmentService : IDepartmentService
    {
        #region Feilds
        private readonly IDepartmentRepository _departmentRepository;
        #endregion

        #region ctors
        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }


        #endregion


        #region Functions

        public async Task<List<Department>> GetAllDepartmentsAsync()
        {
            var dept = await _departmentRepository.GetAllDepartmentAsync();
            return dept;
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var dept = await _departmentRepository.GetTableNoTracking()
                .Include(d => d.Instructors)
                .Include(d => d.DepartmentSubjects)
                .ThenInclude(d => d.Subject)
                .Include(d => d.Instructor)
                .FirstOrDefaultAsync(d => d.DID == id);
            return dept;

        }
        #endregion



    }
}

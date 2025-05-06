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

        public async Task<bool> IsDepartmentNameExist(string name)
        {
            var dept = await _departmentRepository.GetTableNoTracking().Where(s => s.DNameEn.Equals(name)).FirstOrDefaultAsync();
            if (dept is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<string> AddDepartmentAsync(Department department)
        {
            var result = await IsDepartmentNameExist(department.DNameEn);
            if (result)
            {
                return ("Department already exists");
            }
            else
            {
                await _departmentRepository.AddAsync(department);
                return ("Department added successfully");
            }


        }

        public async Task<bool> IsManagerExist(int id)
        {
            var dept = _departmentRepository.GetTableNoTracking().Where(d => d.InsManagerId == id).FirstOrDefault();

            if (dept is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> UpdateDepartmentAsync(Department department)
        {

            await _departmentRepository.UpdateAsync(department);

            return ("Department updated successfully");

            //var transaction = _departmentRepository.BeginTransaction();
            //try
            //{

            //    var Existingdept = await GetDepartmentByIdAsync(department.DID);
            //    if (Existingdept is null)
            //        return ("Department not found");
            //    var nameExist = await IsDepartmentNameExist(department.DNameEn);
            //    if (nameExist && department.DNameEn != Existingdept.DNameEn)
            //        return ("Department already exists");
            //    else
            //    {
            //        await _departmentRepository.UpdateAsync(department);
            //        await transaction.CommitAsync();
            //        return ("Department updated successfully");


            //    }

            //}
            //catch (Exception ex)
            //{
            //    await transaction.RollbackAsync();
            //    return ("An error occurred while updating the department");
            //}
            //finally
            //{
            //    await transaction.DisposeAsync();

            //}
        }

        public Task<Department> GetDepartmentByIdAsyncWithoutInclude(int id)
        {
            var dept = _departmentRepository.GetTableNoTracking()
                .FirstOrDefaultAsync(d => d.DID == id);
            return dept;
        }

        #endregion



    }
}

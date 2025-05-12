using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentByIdAsync(int id);
        Task<Department> GetDepartmentByIdAsyncWithoutInclude(int id);
        Task<List<Department>> GetAllDepartmentsAsync();
        Task<string> AddDepartmentAsync(Department department);
        Task<string> UpdateDepartmentAsync(Department department);
        Task<string> DeleteDepartmentAsync(int id);

        Task<bool> IsManagerExist(int id);
        Task<bool> IsDepartmentIdExist(int id);

    }
}

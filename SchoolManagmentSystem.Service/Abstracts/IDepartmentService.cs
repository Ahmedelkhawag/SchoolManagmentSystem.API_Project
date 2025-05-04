using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IDepartmentService
    {
        Task<Department> GetDepartmentByIdAsync(int id);
    }
}

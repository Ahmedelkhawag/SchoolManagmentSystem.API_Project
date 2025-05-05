using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Abstract;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Interfaces
{
    public interface IDepartmentRepository : IGenericRepositoryAsync<Department>
    {
        Task<List<Department>> GetAllDepartmentAsync();
    }
}

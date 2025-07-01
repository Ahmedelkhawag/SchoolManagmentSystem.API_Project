using SchoolManagmentSystem.Data.DTOs;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IAutorizationService
    {
        Task<string> AddRoleAsync(string roleName);
        Task<bool> IsRoleExist(string roleName);
        Task<string> EditRoleAsync(EditRoleRequest editRoleRequest);
        Task<string> DeleteRoleAsync(int id);
        Task<bool> RoleCanBeDeleted(int roleId);
    }
}

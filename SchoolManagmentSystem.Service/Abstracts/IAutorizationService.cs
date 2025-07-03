using SchoolManagmentSystem.Data.DTOs;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IAutorizationService
    {
        Task<string> AddRoleAsync(string roleName);
        Task<bool> IsRoleExist(string roleName);
        Task<string> EditRoleAsync(EditRoleRequest editRoleRequest);
        Task<string> DeleteRoleAsync(int id);
        Task<bool> RoleCanBeDeleted(int roleId);
        Task<List<ApplicationRole>> GetRolesAsync();
        Task<ApplicationRole> GetRoleByIdAsync(int roleId);
        Task<ManageUserRolesResult> GetUserRolesAsync(int userId);
    }
}

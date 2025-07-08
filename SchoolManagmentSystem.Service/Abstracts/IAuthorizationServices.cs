using SchoolManagmentSystem.Data.DTOs;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Requests;
using SchoolManagmentSystem.Data.Results;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IAuthorizationServices
    {
        Task<string> AddRoleAsync(string roleName);
        Task<bool> IsRoleExist(string roleName);
        Task<string> EditRoleAsync(EditRoleRequest editRoleRequest);
        Task<string> DeleteRoleAsync(int id);
        Task<bool> RoleCanBeDeleted(int roleId);
        Task<List<ApplicationRole>> GetRolesAsync();
        Task<ApplicationRole> GetRoleByIdAsync(int roleId);
        Task<ManageUserRolesResult> GetUserRolesAsync(int userId);
        Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request);
        Task<ManageUserClaimsResult> ManageUserClaimsAsync(int userId);
        Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest request);
    }
}

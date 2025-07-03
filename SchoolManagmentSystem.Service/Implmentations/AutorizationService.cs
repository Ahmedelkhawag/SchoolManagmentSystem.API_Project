using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.DTOs;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class AutorizationService : IAutorizationService
    {
        #region Feilds
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        #endregion


        #region Ctor

        public AutorizationService(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        #endregion

        #region Functions


        public async Task<string> AddRoleAsync(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty.", nameof(roleName));
            }

            if (await IsRoleExist(roleName))
            {
                return "Role already exists.";
            }

            var result = await _roleManager.CreateAsync(new ApplicationRole(roleName));
            if (result.Succeeded)
            {
                return "Role created successfully.";
            }

            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return $"Failed to create role: {errors}";
        }



        public async Task<bool> IsRoleExist(string roleName)
        {

            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentException("Role name cannot be null or empty.", nameof(roleName));
            }
            return await _roleManager.RoleExistsAsync(roleName);
        }

        public async Task<string> EditRoleAsync(EditRoleRequest editRoleRequest)
        {
            // check if the role exists
            if (editRoleRequest == null || string.IsNullOrWhiteSpace(editRoleRequest.RoleName) || editRoleRequest.Id <= 0)
            {
                throw new ArgumentException("Invalid role request.", nameof(editRoleRequest));
            }
            // Find the role by ID
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == editRoleRequest.Id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {editRoleRequest.Id} not found.");
            }

            // Update the role name
            role.Name = editRoleRequest.RoleName;

            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return "Role updated successfully.";
            }
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return $"Failed to update role: {errors}";

        }

        public async Task<string> DeleteRoleAsync(int id)
        {

            var canDeleted = await RoleCanBeDeleted(id);
            if (!canDeleted)
            {
                return "Role cannot be deleted because it has users assigned to it.";
            }
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == id);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {id} not found.");
            }
            // Delete the role
            var result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return "Role deleted successfully.";
            }
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return $"Failed to delete role: {errors}";
        }

        public async Task<bool> RoleCanBeDeleted(int roleId)
        {

            if (roleId <= 0)
            {
                throw new ArgumentException("Invalid role ID.", nameof(roleId));
            }
            // Check if the role exists
            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
            if (role == null)
            {
                throw new KeyNotFoundException($"Role with ID {roleId} not found.");
            }
            // Check if the role has any users assigned to it
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);
            return !usersInRole.Any();
        }

        public async Task<List<ApplicationRole>> GetRolesAsync()
        {

            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<ApplicationRole> GetRoleByIdAsync(int roleId)
        {
            if (roleId <= 0)
            {
                throw new ArgumentException("Invalid role ID.", nameof(roleId));
            }
            return await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == roleId);
        }

        public async Task<ManageUserRolesResult> GetUserRolesAsync(int userId)
        {
            // Validate userId
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }
            var allRoles = await _roleManager.Roles.ToListAsync();

            // Get the roles for the user
            var roles = await _userManager.GetRolesAsync(user);

            // Map the roles to UserRoles DTO
            var userRoles = allRoles?.Select(role => new UserRoles
            {
                RoleId = role.Id,
                RoleName = role.Name,
                IsSelected = roles.Contains(role.Name)
            }).ToList() ?? new List<UserRoles>();

            return new ManageUserRolesResult
            {
                userId = user.Id,
                userRoles = userRoles
            };
        }
        #endregion
    }
}


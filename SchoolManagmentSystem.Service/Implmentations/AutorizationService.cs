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
        #endregion


        #region Ctor

        public AutorizationService(RoleManager<ApplicationRole> roleManager)
        {
            _roleManager = roleManager;
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
        #endregion
    }
}

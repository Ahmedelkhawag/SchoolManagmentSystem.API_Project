using Microsoft.AspNetCore.Identity;
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

        #endregion
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
    }
}

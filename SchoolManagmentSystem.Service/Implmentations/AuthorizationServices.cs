using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.DTOs;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Data.Requests;
using SchoolManagmentSystem.Data.Results;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Service.Abstracts;
using System.Security.Claims;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class AuthorizationServices : IAuthorizationServices
    {
        #region Feilds
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        #endregion


        #region Ctor

        public AuthorizationServices(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _context = context;
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
            // Return the result
            return new ManageUserRolesResult
            {
                userId = user.Id,
                userRoles = userRoles
            };
        }

        public async Task<string> UpdateUserRolesAsync(UpdateUserRolesRequest request)
        {
            // Validate request
            var user = await _userManager.FindByIdAsync(request.userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {request.userId} not found.");
            }
            // Get the current roles of the user
            var currentUserRoles = await _userManager.GetRolesAsync(user);

            // Get the roles to be added
            var selectedRoles = request.userRoles
                .Where(r => r.IsSelected == true)
                .Select(r => r.RoleName)
                .ToList();

            //var AddedRoles = selectedRoles.Except(currentUserRoles).ToList();
            var rolesToRemove = currentUserRoles.Except(selectedRoles).ToList();


            // Add new roles to the user
            if (selectedRoles.Any())
            {
                var addResult = await _userManager.AddToRolesAsync(user, selectedRoles);
                if (!addResult.Succeeded)
                {
                    var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                    return $"Failed to add roles: {errors}";
                }
            }

            // Remove roles from the user

            if (rolesToRemove.Any())
            {
                var removeResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
                if (!removeResult.Succeeded)
                {
                    var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                    return $"Failed to remove roles: {errors}";
                }
            }
            return $"User roles updated successfully for user ID: {request.userId}";
        }

        public async Task<ManageUserClaimsResult> ManageUserClaimsAsync(int userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {userId} not found.");
            }


            var existingUserCliams = await _userManager.GetClaimsAsync(user);

            var model = new ManageUserClaimsResult
            {
                userId = userId,
                userClaims = new List<UserClaim>()
            };

            foreach (var claim in ClaimsStore.Claims)
            {
                var userClaim = new UserClaim
                {
                    ClaimType = claim.Type,
                    ClaimValue = claim.Value,
                    IsSelected = existingUserCliams.Any(c => c.Type == claim.Type && c.Value == claim.Value)
                };
                model.userClaims.Add(userClaim);
            }
            return model;

        }

        public async Task<string> UpdateUserClaimsAsync(UpdateUserClaimsRequest request)
        {
            var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // Validate request
                var user = await _userManager.FindByIdAsync(request.userId.ToString());
                if (user == null)
                {
                    throw new KeyNotFoundException($"User with ID {request.userId} not found.");
                }
                // Get the current claims of the user
                var currentUserClaims = await _userManager.GetClaimsAsync(user);
                // Get the selected claims from the request
                var selectedClaims = request.userClaims
                    .Where(c => c.IsSelected == true)
                    .Select(c => new Claim(c.ClaimType, c.ClaimValue))
                    .ToList();
                // Get the claims to be added
                var claimsToBeAdded = selectedClaims
                    .Where(sc => !currentUserClaims.Any(c => c.Type == sc.Type && c.Value == sc.Value))
                    .ToList();
                // Get the claims to be removed
                var claimsToBeRemoved = currentUserClaims
                    .Where(c => !selectedClaims.Any(sc => sc.Type == c.Type && sc.Value == c.Value))
                    .ToList();

                // Add new claims to the user
                if (claimsToBeAdded.Any())
                {
                    var addResult = await _userManager.AddClaimsAsync(user, claimsToBeAdded);
                    if (!addResult.Succeeded)
                    {
                        var errors = string.Join(", ", addResult.Errors.Select(e => e.Description));
                        return $"Failed to add claims: {errors}";
                    }
                }

                // Remove claims from the user
                if (claimsToBeRemoved.Any())
                {
                    var removeResult = await _userManager.RemoveClaimsAsync(user, claimsToBeRemoved);
                    if (!removeResult.Succeeded)
                    {
                        var errors = string.Join(", ", removeResult.Errors.Select(e => e.Description));
                        return $"Failed to remove claims: {errors}";
                    }
                }
                await transaction.CommitAsync();
                return $"User claims updated successfully for user ID: {request.userId}";
            }
            catch (Exception ex)
            {

                await transaction.RollbackAsync();
                return $"Failed to update claims: An error occurred while processing user ID: {request.userId}. Error details: {ex.Message}";
            }



        }
        #endregion
    }
}



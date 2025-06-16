using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Infrastructure.SeedingData
{
    public static class RoleSeeding
    {
        public static async Task SeedRoles(RoleManager<ApplicationRole> _roleManager)
        {

            var roles = await _roleManager.Roles.CountAsync();
            if (roles <= 0)
            {
                var roleList = new List<ApplicationRole>
                {
                    new ApplicationRole("Admin"),
                    new ApplicationRole("User"),
                    new ApplicationRole("Student"),
                    new ApplicationRole("Teacher")
                };

                foreach (var role in roleList)
                {
                    await _roleManager.CreateAsync(role);
                }
            }
        }
    }
}

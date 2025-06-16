using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Infrastructure.SeedingData
{
    public static class UserSeeding
    {
        public async static Task SeedUsers(UserManager<ApplicationUser> _userManager)
        {
            var userCount = await _userManager.Users.CountAsync();

            if (userCount <= 0)
            {
                var user = new ApplicationUser
                {
                    UserName = "exampleUser",
                    Email = "example@gmail.com",
                    FirstName = "Example",
                    LastName = "User",
                    Address = "123 Example St",
                    PhoneNumber = "123-456-7890",
                    Country = "Exampleland",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };

                await _userManager.CreateAsync(user, "Password123!");
                await _userManager.AddToRoleAsync(user, "Admin");
            }
        }
    }
}

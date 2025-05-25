using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace SchoolManagmentSystem.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public ApplicationUser()
        {
            UserRefreshTokens = new HashSet<UserRefreshToken>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Country { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }

    }

}

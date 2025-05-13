using Microsoft.AspNetCore.Identity;

namespace SchoolManagmentSystem.Data.Entities.Identity
{
    public class ApplicationUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

    }

}

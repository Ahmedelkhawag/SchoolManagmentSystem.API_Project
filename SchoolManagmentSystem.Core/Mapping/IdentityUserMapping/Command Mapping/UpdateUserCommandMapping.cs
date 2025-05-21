using SchoolManagmentSystem.Core.Features.User.Commands.Models;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Core.Mapping.IdentityUserMapping
{
    public partial class UserProfile
    {
        public void UpdateUserMapping()
        {
            CreateMap<UpdateUserCommand, ApplicationUser>();
        }
    }
}

using SchoolManagmentSystem.Core.Features.User.Queries.Result;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Core.Mapping.IdentityUserMapping
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<ApplicationUser, GetUserByIdQueryResponse>();
        }
    }
}

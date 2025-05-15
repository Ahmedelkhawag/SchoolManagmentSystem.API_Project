using AutoMapper;

namespace SchoolManagmentSystem.Core.Mapping.IdentityUserMapping
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserMapping();
        }
    }
}

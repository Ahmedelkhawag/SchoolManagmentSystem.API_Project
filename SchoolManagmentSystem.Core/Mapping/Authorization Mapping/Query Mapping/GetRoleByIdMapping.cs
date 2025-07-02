using SchoolManagmentSystem.Core.Features.Authorization.Queries.Result;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Core.Mapping.Authorization_Mapping
{
    public partial class AuthorizationProfile
    {
        public void GetRoleByIdMapping()
        {
            CreateMap<ApplicationRole, GetRoleByIdResult>()
                .ForMember(dest => dest.roleName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.roleId, opt => opt.MapFrom(src => src.Id));
        }
    }
}

using AutoMapper;

namespace SchoolManagmentSystem.Core.Mapping.Authorization_Mapping
{
    public partial class AuthorizationProfile : Profile
    {
        public AuthorizationProfile()
        {
            GetRulesMapping();
            GetRoleByIdMapping();
        }
    }
}

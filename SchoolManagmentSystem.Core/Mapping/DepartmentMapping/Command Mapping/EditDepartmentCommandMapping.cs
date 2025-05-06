using SchoolManagmentSystem.Core.Features.Departments.Commands.Models;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void EditDepartmentMapping()
        {
            CreateMap<EditDepartmentCommand, Department>()
                .ForMember(dest => dest.DID, options => options.MapFrom(src => src.Id))
                .ForMember(dest => dest.DNameAr, options => options.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.DNameEn, options => options.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.InsManagerId, options => options.MapFrom(src => src.ManagerId));
        }
    }
}

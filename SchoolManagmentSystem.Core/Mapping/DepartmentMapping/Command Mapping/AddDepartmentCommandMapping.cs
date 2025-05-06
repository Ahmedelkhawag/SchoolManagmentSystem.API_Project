using SchoolManagmentSystem.Core.Features.Departments.Commands.Models;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void AddDepartmentMapping()
        {
            CreateMap<AddDepartmentCommand, Department>()
                .ForMember(dest => dest.DNameAr, options => options.MapFrom(src => src.NameAr))
                .ForMember(dest => dest.DNameEn, options => options.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.InsManagerId, options => options.MapFrom(src => src.ManagerId));
        }
    }

}

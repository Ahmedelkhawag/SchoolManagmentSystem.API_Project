using SchoolManagmentSystem.Core.Features.Departments.Queries.Results;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetAllDepartmentsWithOutIncludeMapping()
        {

            CreateMap<Department, GetAllDepartmentsWithOutIncludeQueryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetGeneralLocalizedEntity(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.GetGeneralLocalizedEntity(src.Instructor.ENameAr, src.Instructor.ENameEn) : src.Instructor.ENameEn));

        }
    }
}

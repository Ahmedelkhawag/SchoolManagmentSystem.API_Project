using SchoolManagmentSystem.Core.Features.students.Queries.Results;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Core.Mapping.StudentsMapping
{


    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>()
                   .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department.GetGeneralLocalizedEntity(src.Department.DNameAr, src.Department.DNameEn)))
                   .ForMember(dest => dest.Name, options => options.MapFrom(src => src.GetGeneralLocalizedEntity(src.NameAr, src.NameEn)));

            //CreateMap<Student, GetSingleStudentResponse>()
            //     .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department.DName));
        }
    }
}

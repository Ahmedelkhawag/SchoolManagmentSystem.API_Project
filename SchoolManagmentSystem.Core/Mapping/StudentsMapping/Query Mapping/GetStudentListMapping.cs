using SchoolManagmentSystem.Core.Features.students.Queries.Results;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Core.Mapping.StudentsMapping
{


    public partial class StudentProfile
    {
        public void GetStudentListMapping()
        {
            CreateMap<Student, GetStudentListResponse>()
                   .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department.DNameEn));

            //CreateMap<Student, GetSingleStudentResponse>()
            //     .ForMember(dest => dest.DepartmentName, options => options.MapFrom(src => src.Department.DName));
        }
    }
}

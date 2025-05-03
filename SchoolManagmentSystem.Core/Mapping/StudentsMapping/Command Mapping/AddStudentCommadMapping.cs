using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Core.Mapping.StudentsMapping
{
    public partial class StudentProfile
    {
        public void AddStudentMapping()
        {
            CreateMap<AddStudentCommand, Student>()
                    .ForMember(dest => dest.DID, options => options.MapFrom(src => src.DepartmentId))
                    .ForMember(dest => dest.NameAr, options => options.MapFrom(src => src.NameAr))
                    .ForMember(dest => dest.NameEn, options => options.MapFrom(src => src.NameEn));
        }
    }
}

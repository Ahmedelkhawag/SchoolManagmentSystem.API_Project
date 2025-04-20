using AutoMapper;

namespace SchoolManagmentSystem.Core.Mapping.StudentsMapping
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentListMapping();
            GetStudentByIdMapping();
            AddStudentMapping();
            EditStudentMapping();
        }
    }
}

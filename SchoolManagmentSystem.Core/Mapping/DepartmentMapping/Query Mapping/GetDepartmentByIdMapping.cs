using SchoolManagmentSystem.Core.Features.Departments.Queries.Results;
using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile
    {
        public void GetDeptByIdMapping()
        {
            CreateMap<Department, GetDepartmentByIdQueryResponse>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.DID))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetGeneralLocalizedEntity(src.DNameAr, src.DNameEn)))
                .ForMember(dest => dest.ManagerName, opt => opt.MapFrom(src => src.Instructor != null ? src.Instructor.GetGeneralLocalizedEntity(src.Instructor.ENameAr, src.Instructor.ENameEn) : src.Instructor.ENameEn))
                .ForMember(dest => dest.StudentList, opt => opt.MapFrom(src => src.Students.Select(s => new StudentResponse
                {
                    Id = s.StudID,
                    Name = s.GetGeneralLocalizedEntity(s.NameAr, s.NameEn)
                }).ToList()))
                .ForMember(dest => dest.InstructorList, opt => opt.MapFrom(src => src.Instructors.Select(i => new InstructorResponse
                {
                    Id = i.InsId,
                    Name = i.GetGeneralLocalizedEntity(i.ENameAr, i.ENameEn)
                }).ToList()))
                .ForMember(dest => dest.SubjectList, opt => opt.MapFrom(src => src.DepartmentSubjects.Select(s => new SubjectResponse
                {
                    Id = s.SubID,
                    Name = s.GetGeneralLocalizedEntity(s.Subject.SubjectNameAr, s.Subject.SubjectNameEn)
                }).ToList()));



        }
    }
}

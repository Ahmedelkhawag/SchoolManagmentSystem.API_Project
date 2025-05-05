using AutoMapper;

namespace SchoolManagmentSystem.Core.Mapping.DepartmentMapping
{
    public partial class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            GetDeptByIdMapping();
            GetAllDepartmentsWithOutIncludeMapping();
        }
    }
}

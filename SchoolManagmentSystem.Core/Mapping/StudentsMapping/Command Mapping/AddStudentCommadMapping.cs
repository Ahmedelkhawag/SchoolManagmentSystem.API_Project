using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Mapping.StudentsMapping
{
    public partial class StudentProfile
    {
        public void AddStudentMapping()
        {
            CreateMap<AddStudentCommand, Student>()
                    .ForMember(dest => dest.DID, options => options.MapFrom(src => src.DepartmentId));
        }
    }
}

﻿using AutoMapper;
using SchoolManagmentSystem.Core.Features.students.Queries.Results;
using SchoolManagmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Mapping.StudentsMapping
{
    public partial class StudentProfile : Profile
    {
        public StudentProfile()
        {
            GetStudentListMapping();
            GetStudentByIdMapping();
            AddStudentMapping();
        }
    }
}

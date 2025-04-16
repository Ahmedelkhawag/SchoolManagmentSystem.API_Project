
using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.students.Queries.Results;
using SchoolManagmentSystem.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Features.students.Queries.Models
{
    public class GetStudentsListQuery : IRequest<Response<List<GetStudentListResponse>>>
    {  
    }
   
}

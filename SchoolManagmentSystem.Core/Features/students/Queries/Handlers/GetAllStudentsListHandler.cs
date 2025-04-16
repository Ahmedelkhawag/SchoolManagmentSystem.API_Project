using MediatR;
using SchoolManagmentSystem.Core.Features.students.Queries.Models;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using SchoolManagmentSystem.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Features.students.Queries.Handlers
{
    public class GetAllStudentsListHandler:IRequestHandler<GetStudentsListQuery, List<Student>>
    {
        #region Fields
        private readonly IStudentService  _studentService;

        #endregion

        #region Ctors
        public GetAllStudentsListHandler(IStudentService studentService)
        {
            _studentService = studentService;
        }

        #endregion

        #region Interface Implmentations
        public async Task<List<Student>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            return await _studentService.GetAllStudentsAsync();
        }

        #endregion
    }
    
}

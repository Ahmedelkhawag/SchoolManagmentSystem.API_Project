using AutoMapper;
using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.students.Queries.Models;
using SchoolManagmentSystem.Core.Features.students.Queries.Results;
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
    public class GetAllStudentsListHandler:ResponseHandler , IRequestHandler<GetStudentsListQuery, Response<List<GetStudentListResponse>>>
    {
        #region Fields
        private readonly IStudentService  _studentService;
        private readonly IMapper _mapper;

        #endregion

        #region Ctors
        public GetAllStudentsListHandler(IStudentService studentService , IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        #endregion

        #region Interface Implmentations
        public async Task<Response<List<GetStudentListResponse>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            var studentlist =  await _studentService.GetAllStudentsAsync();
            var mappedStudentList = _mapper.Map<List<GetStudentListResponse>>(studentlist);
            return Success(mappedStudentList);
        }

        #endregion
    }
    
}

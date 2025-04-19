using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Features.students.Commads.Handlers
{
    public class StudentCommandHandler : ResponseHandler, IRequestHandler<AddStudentCommand, GeneralResponse<string>>
    {
        #region props
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;


        #endregion

        #region Ctors
        public StudentCommandHandler(IStudentService studentService, IMapper mapper)
        {
            _studentService = studentService;
            _mapper = mapper;
        }

        #endregion

        #region Methods
        public async Task<GeneralResponse<string>> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var mappedStudent = _mapper.Map<Student>(request);
            var result = await _studentService.AddStudentAsync(mappedStudent);
            if (result.Contains("exist"))
            {
                return UnprocessableEntity<string>("Student Not Added");
            }

            else if (result.Contains("successfully")) return Created<string>(result);

            else
            {
                return BadRequest<string>();
            }
        }
        #endregion
    }
}

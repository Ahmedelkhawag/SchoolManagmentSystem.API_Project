using AutoMapper;
using MediatR;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.students.Commads.Models;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.students.Commads.Handlers
{
    public class StudentCommandHandler : ResponseHandler,
        IRequestHandler<AddStudentCommand, GeneralResponse<string>>,
        IRequestHandler<EditStudentCommand, GeneralResponse<string>>,
        IRequestHandler<DeleteStudentCommand, GeneralResponse<string>>
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


            if (result.Contains("successfully")) return Created<string>(result);

            else
            {
                return BadRequest<string>();
            }
        }

        public async Task<GeneralResponse<string>> Handle(EditStudentCommand request, CancellationToken cancellationToken)
        {
            var ExistingStudent = await _studentService.GetById(request.Id);
            if (ExistingStudent == null) return NotFound<string>("Student is not found");
            else
            {
                var mappedStudent = _mapper.Map<EditStudentCommand, Student>(request, ExistingStudent);
                var result = await _studentService.UpdateStudentAsync(mappedStudent);
                if (result.Contains("successfully")) return NoContent<string>(result);
                else
                {
                    return BadRequest<string>();
                }

            }
        }

        public async Task<GeneralResponse<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            var result = await _studentService.DeleteStudentAsync(request.Id);
            if (result.Contains("successfully")) return Deleted<string>(result);
            else
            {
                return NotFound<string>();
            }

        }
        #endregion
    }
}

using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Departments.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Departments.Commands.Handlers
{
    public class DepartmentCommandHandler : ResponseHandler, IRequestHandler<AddDepartmentCommand, GeneralResponse<string>>,
         IRequestHandler<EditDepartmentCommand, GeneralResponse<string>>,
            IRequestHandler<DeleteDepartmentCommand, GeneralResponse<string>>



    {
        #region props
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourse> _localizer;
        #endregion
        #region Ctors
        public DepartmentCommandHandler(IDepartmentService departmentService, IMapper mapper, IStringLocalizer<SharedResourse> localizer) : base(localizer)
        {
            _departmentService = departmentService;
            _mapper = mapper;
            _localizer = localizer;
        }
        #endregion  
        #region Methods
        public async Task<GeneralResponse<string>> Handle(AddDepartmentCommand request, CancellationToken cancellationToken)
        {

            var mappedDepartment = _mapper.Map<Department>(request);
            var result = await _departmentService.AddDepartmentAsync(mappedDepartment);
            if (result.Contains("successfully")) return Created<string>(result);
            else
            {
                return BadRequest<string>();
            }
        }

        public async Task<GeneralResponse<string>> Handle(EditDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Check if the department exists
            var ExistingDepartment = await _departmentService.GetDepartmentByIdAsyncWithoutInclude(request.Id);

            if (ExistingDepartment is null)
                return NotFound<string>(_localizer[SharedResourseKeys.NotFound]);
            //Map the request to the existing department
            //  var mappedDepartment = _mapper.Map<EditDepartmentCommand, Department>(request, ExistingDepartment);
            _mapper.Map(request, ExistingDepartment);
            var result = await _departmentService.UpdateDepartmentAsync(ExistingDepartment);
            if (result.Contains("successfully")) return NoContent<string>(result);
            else
            {
                return BadRequest<string>();
            }

        }

        public async Task<GeneralResponse<string>> Handle(DeleteDepartmentCommand request, CancellationToken cancellationToken)
        {
            // Check if the department exists
            var ExistingDepartment = await _departmentService.GetDepartmentByIdAsyncWithoutInclude(request.Id);
            if (ExistingDepartment is null)
                return NotFound<string>(_localizer[SharedResourseKeys.NotFound]);
            // Check if the department has a manager
            var isManagerExist = await _departmentService.IsManagerExist(request.Id);
            if (isManagerExist)
                return UnprocessableEntity<string>(_localizer[SharedResourseKeys.Unprocessable]);
            // Delete the department
            var result = await _departmentService.DeleteDepartmentAsync(ExistingDepartment.DID);
            if (result.Contains("successfully")) return Deleted<string>(result);
            else
            {
                return BadRequest<string>();
            }

        }
        #endregion
    }
}



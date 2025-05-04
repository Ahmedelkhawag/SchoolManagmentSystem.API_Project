using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Models;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Results;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, GeneralResponse<GetDepartmentByIdQueryResponse>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly IMapper _mapper;
        #endregion

        #region Ctors
        public DepartmentQueryHandler(IDepartmentService departmentService, IStringLocalizer<SharedResourse> localizer, IMapper mapper) : base(localizer)
        {
            _departmentService = departmentService;
            _localizer = localizer;
            _mapper = mapper;
        }

        #endregion

        #region Handlers
        public async Task<GeneralResponse<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _departmentService.GetDepartmentByIdAsync(request.Id);
            if (response == null)
            {
                return NotFound<GetDepartmentByIdQueryResponse>(_localizer[SharedResourseKeys.NotFound]);
            }
            var mappedResponse = _mapper.Map<GetDepartmentByIdQueryResponse>(response);
            return Success(mappedResponse);

        }

        #endregion
    }
}

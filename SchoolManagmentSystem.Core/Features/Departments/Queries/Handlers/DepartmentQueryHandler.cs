using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Models;
using SchoolManagmentSystem.Core.Features.Departments.Queries.Results;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Core.Wrappers;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolManagmentSystem.Core.Features.Departments.Queries.Handlers
{
    public class DepartmentQueryHandler : ResponseHandler, IRequestHandler<GetDepartmentByIdQuery, GeneralResponse<GetDepartmentByIdQueryResponse>>,
        IRequestHandler<GetAllDepartmentsWithOutIncludeQuery, GeneralResponse<List<GetAllDepartmentsWithOutIncludeQueryResponse>>>
    {
        #region Fields
        private readonly IDepartmentService _departmentService;
        private readonly IStudentService _studentService;
        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly IMapper _mapper;
        #endregion

        #region Ctors
        public DepartmentQueryHandler(IDepartmentService departmentService, IStudentService studentService, IStringLocalizer<SharedResourse> localizer, IMapper mapper) : base(localizer)
        {
            _departmentService = departmentService;
            _studentService = studentService;
            _localizer = localizer;
            _mapper = mapper;
        }

        #endregion

        #region Handlers
        public async Task<GeneralResponse<GetDepartmentByIdQueryResponse>> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            //Service get by Id including instructors and subjects
            var response = await _departmentService.GetDepartmentByIdAsync(request.Id);
            //Check if response is null
            if (response == null)
            {
                return NotFound<GetDepartmentByIdQueryResponse>(_localizer[SharedResourseKeys.NotFound]);
            }
            //Mapping the response to the GetDepartmentByIdQueryResponse
            var mappedResponse = _mapper.Map<GetDepartmentByIdQueryResponse>(response);
            //Pagination 
            Expression<Func<Student, StudentResponse>> expression = e => new StudentResponse
            {
                Id = e.StudID,
                Name = e.GetGeneralLocalizedEntity(e.NameAr, e.NameEn)
            };
            var studentsQueryable = _studentService.GetAllStudentsByDepartmentIdQueryable(request.Id);
            var paginatedStds = await studentsQueryable.Select(expression).ToPaginatedListAsynd(request.StudentPageNumber, request.StudentPageSize);
            mappedResponse.StudentList = paginatedStds;
            return Success(mappedResponse);

        }

        public async Task<GeneralResponse<List<GetAllDepartmentsWithOutIncludeQueryResponse>>> Handle(GetAllDepartmentsWithOutIncludeQuery request, CancellationToken cancellationToken)
        {
            var response = await _departmentService.GetAllDepartmentsAsync();
            if (response == null)
            {
                return NotFound<List<GetAllDepartmentsWithOutIncludeQueryResponse>>(_localizer[SharedResourseKeys.NotFound]);
            }
            var mappedResponse = _mapper.Map<List<GetAllDepartmentsWithOutIncludeQueryResponse>>(response);
            return Success(mappedResponse);
        }

        #endregion
    }
}

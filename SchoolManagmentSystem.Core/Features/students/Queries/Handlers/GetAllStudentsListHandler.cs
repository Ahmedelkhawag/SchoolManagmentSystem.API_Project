using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.students.Queries.Models;
using SchoolManagmentSystem.Core.Features.students.Queries.Results;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Core.Wrappers;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Service.Abstracts;
using System.Linq.Expressions;

namespace SchoolManagmentSystem.Core.Features.students.Queries.Handlers
{
    public class GetAllStudentsListHandler : ResponseHandler,
        IRequestHandler<GetStudentsListQuery, GeneralResponse<List<GetStudentListResponse>>>,
        IRequestHandler<GetStudentByIdQuery, GeneralResponse<GetSingleStudentResponse>>,
       IRequestHandler<GetStudentListQueryPaginated, PaginatedResult<GetStudentPaginatedListResponse>>
    {
        #region Fields
        private readonly IStudentService _studentService;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResourse> _Localizer;

        #endregion

        #region Ctors
        public GetAllStudentsListHandler(IStudentService studentService, IMapper mapper, IStringLocalizer<SharedResourse> Localizer) : base(Localizer)
        {
            _studentService = studentService;
            _mapper = mapper;
            _Localizer = Localizer;
        }

        #endregion

        #region Interface Implmentations
        public async Task<GeneralResponse<List<GetStudentListResponse>>> Handle(GetStudentsListQuery request, CancellationToken cancellationToken)
        {
            var studentlist = await _studentService.GetAllStudentsAsync();
            var mappedStudentList = _mapper.Map<List<GetStudentListResponse>>(studentlist);
            var result = Success(mappedStudentList);
            result.Meta = new
            {
                TotalCount = mappedStudentList.Count,

            };
            return result;
        }

        public async Task<GeneralResponse<GetSingleStudentResponse>> Handle(GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            var std = await _studentService.GetById(request.Id);

            var mappedStudent = _mapper.Map<GetSingleStudentResponse>(std);
            if (mappedStudent == null)
            {
                return NotFound<GetSingleStudentResponse>(_Localizer[SharedResourseKeys.NotFound]);
            }
            return Success(mappedStudent);

        }

        public async Task<PaginatedResult<GetStudentPaginatedListResponse>> Handle(GetStudentListQueryPaginated request, CancellationToken cancellationToken)
        {
            Expression<Func<Student, GetStudentPaginatedListResponse>> expression = e => new GetStudentPaginatedListResponse(e.StudID, e.GetGeneralLocalizedEntity(e.NameAr, e.NameEn), e.Address, e.Department.GetGeneralLocalizedEntity(e.Department.DNameAr, e.Department.DNameEn));

            var query = _studentService.FilterStudentWithpaginatedQueryable(request.OrderBy, request.Search);
            var Filteredquery = await query.Select(expression).ToPaginatedListAsynd(request.PageNumber, request.PageSize);
            Filteredquery.Meta = new
            {

                TotalCount = Filteredquery.TotalCount,
                PageSize = Filteredquery.PageSize,
                PageNumber = Filteredquery.CurruntPage,
                TotalPages = Filteredquery.TotalPages,
                HasNextPage = Filteredquery.HasNextPage,
                HasPreviousPage = Filteredquery.HasPerviousPage
            };
            return Filteredquery;


        }




        #endregion
    }

}

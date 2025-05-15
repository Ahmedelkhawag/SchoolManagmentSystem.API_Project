using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.User.Queries.Models;
using SchoolManagmentSystem.Core.Features.User.Queries.Result;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Core.Wrappers;
using SchoolManagmentSystem.Data.Entities.Identity;

namespace SchoolManagmentSystem.Core.Features.User.Queries.Handler
{
    public class UserQueryHandler : ResponseHandler, IRequestHandler<GetUsersListQuery, PaginatedResult<GetUsersListQueryResponse>>,
        IRequestHandler<GetUserByIdQuery, GeneralResponse<GetUserByIdQueryResponse>>
    {
        #region Feilds

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IStringLocalizer<SharedResourse> _localizer;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor

        public UserQueryHandler(UserManager<ApplicationUser> userManager, IStringLocalizer<SharedResourse> localizer, IMapper mapper) : base(localizer)
        {
            _userManager = userManager;
            _localizer = localizer;
            _mapper = mapper;
        }
        #endregion

        #region Handlers

        public async Task<PaginatedResult<GetUsersListQueryResponse>> Handle(GetUsersListQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedList = await _mapper.ProjectTo<GetUsersListQueryResponse>(users)
                .ToPaginatedListAsynd(request.PageNumber, request.PageSize);

            return paginatedList;

        }

        public async Task<GeneralResponse<GetUserByIdQueryResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == request.Id);
            if (user == null)
            {
                return NotFound<GetUserByIdQueryResponse>(_localizer[SharedResourseKeys.NotFound]);
            }
            var mappedUser = _mapper.Map<GetUserByIdQueryResponse>(user);
            return Success(mappedUser);

        }





        #endregion
    }
}

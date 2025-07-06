using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authorization.Queries.Models;
using SchoolManagmentSystem.Core.Features.Authorization.Queries.Result;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Results;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authorization.Queries.Handlers
{
    public class RoleQueryHandler : ResponseHandler, IRequestHandler<GetRulesListQuery, GeneralResponse<List<GetRulesListResult>>>,
        IRequestHandler<GetRoleByIdQuery, GeneralResponse<GetRoleByIdResult>>,
        IRequestHandler<ManageUserRolesQuery, GeneralResponse<ManageUserRolesResult>>
    {


        #region Fields


        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IAuthorizationServices _authorizationService;
        private readonly IMapper _mapper;

        #endregion

        #region Ctor
        public RoleQueryHandler(IStringLocalizer<SharedResourse> stringLocalizer,
            IAuthorizationServices authorizationService, IMapper mapper) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            _mapper = mapper;
        }

        #endregion

        #region Functions

        public async Task<GeneralResponse<List<GetRulesListResult>>> Handle(GetRulesListQuery request,
            CancellationToken cancellationToken)
        {
            var roles = await _authorizationService.GetRolesAsync();
            if (roles == null || !roles.Any())
            {
                return NotFound<List<GetRulesListResult>>(_stringLocalizer["NoRolesFound"]);

            }

            var mappedRoles = _mapper.Map<List<GetRulesListResult>>(roles);
            return Success(mappedRoles);
        }

        public async Task<GeneralResponse<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
        {
            var role = await _authorizationService.GetRoleByIdAsync(request.Id);
            if (role == null)
            {
                return NotFound<GetRoleByIdResult>(_stringLocalizer["RoleNotFound"]);
            }
            var mappedRole = _mapper.Map<GetRoleByIdResult>(role);
            return Success(mappedRole);
        }

        public async Task<GeneralResponse<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
        {
            // get user roles by user id
            var userRolesResult = await _authorizationService.GetUserRolesAsync(request.UserId);
            //return success response if user roles found
            return Success(userRolesResult);

        }

        #endregion
    }
}

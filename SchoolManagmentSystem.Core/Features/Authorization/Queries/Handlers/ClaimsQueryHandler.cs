using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authorization.Queries.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Data.Entities.Identity;
using SchoolManagmentSystem.Data.Results;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authorization.Queries.Handlers
{
    public class ClaimsQueryHandler : ResponseHandler, IRequestHandler<ManageUSerClaimsQuery, GeneralResponse<ManageUserClaimsResult>>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly IAuthorizationServices _autorizationService;
        #endregion
        #region Ctor
        public ClaimsQueryHandler(IStringLocalizer<SharedResourse> stringLocalizer,
            UserManager<ApplicationUser> userManager,
            RoleManager<ApplicationRole> roleManager,
            IAuthorizationServices autorizationService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _userManager = userManager;
            _roleManager = roleManager;
            _autorizationService = autorizationService;
        }
        #endregion

        #region handlers

        public async Task<GeneralResponse<ManageUserClaimsResult>> Handle(ManageUSerClaimsQuery request, CancellationToken cancellationToken)
        {
            var result = await _autorizationService.ManageUserClaimsAsync(request.userId);

            return result != null
                ? Success<ManageUserClaimsResult>(result, _stringLocalizer["ClaimsRetrievedSuccessfully"])
                : BadRequest<ManageUserClaimsResult>(_stringLocalizer["ClaimsNotFound"]);
        }
        #endregion
    }
}

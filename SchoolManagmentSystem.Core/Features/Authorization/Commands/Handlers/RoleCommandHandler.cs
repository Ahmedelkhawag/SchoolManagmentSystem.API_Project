using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authorization.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Handlers
{
    public class RoleCommandHandler : ResponseHandler, IRequestHandler<AddRoleCommand, GeneralResponse<string>>,
        IRequestHandler<EditRoleCommand, GeneralResponse<string>>,
        IRequestHandler<DeleteRoleCommand, GeneralResponse<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        // private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IAutorizationService _authorizationService;

        #endregion
        #region Ctor
        public RoleCommandHandler(IStringLocalizer<SharedResourse> stringLocalizer,
            IAutorizationService authorizationService
            ) : base(stringLocalizer)
        {


            _stringLocalizer = stringLocalizer;
            _authorizationService = authorizationService;
            //_roleManager = roleManager;
        }
        #endregion

        #region Functions
        public async Task<GeneralResponse<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrWhiteSpace(request.RoleName))
            {
                return BadRequest<string>(_stringLocalizer["RoleNameRequired"]);
            }

            var result = await _authorizationService.AddRoleAsync(request.RoleName);

            return result.Contains("successfully")
                ? Success(result, _stringLocalizer["Role Created Successfully"])
                : BadRequest<string>(_stringLocalizer["RoleCreationFailed"]);
        }

        public async Task<GeneralResponse<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.EditRoleAsync(request);

            return result.Contains("successfully")
                ? Success(result, _stringLocalizer["Role Edited Successfully"])
                : BadRequest<string>(_stringLocalizer["RoleEditFailed"]);
        }

        public async Task<GeneralResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationService.DeleteRoleAsync(request.RoleId);

            return result.Contains("successfully")
                ? Success(result, _stringLocalizer["Role Deleted Successfully"])
                : BadRequest<string>(_stringLocalizer["RoleDeletionFailed"]);
        }
        #endregion
    }
}

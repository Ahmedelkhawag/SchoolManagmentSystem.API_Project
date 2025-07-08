using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Authorization.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Authorization.Commands.Handlers
{
    public class ClaimsCommandHandler : ResponseHandler, IRequestHandler<UpdateUserClaimsCommand, GeneralResponse<string>>
    {
        #region Fields

        private readonly IStringLocalizer<SharedResourse> _StringLocalizer;
        private readonly IAuthorizationServices _authorizationServices;
        #endregion

        #region ctor

        public ClaimsCommandHandler(IStringLocalizer<SharedResourse> stringLocalizer, IAuthorizationServices authorizationServices) :
            base(stringLocalizer)
        {
            _StringLocalizer = stringLocalizer;
            _authorizationServices = authorizationServices;
        }

        #endregion

        #region Handlers

        public async Task<GeneralResponse<string>> Handle(UpdateUserClaimsCommand request, CancellationToken cancellationToken)
        {
            var result = await _authorizationServices.UpdateUserClaimsAsync(request);
            return result.Contains("successfully")
                ? Success(result, _StringLocalizer[SharedResourseKeys.Succeeded])
                : BadRequest<string>(_StringLocalizer[SharedResourseKeys.BadRequest]);

        }
        #endregion
    }
}

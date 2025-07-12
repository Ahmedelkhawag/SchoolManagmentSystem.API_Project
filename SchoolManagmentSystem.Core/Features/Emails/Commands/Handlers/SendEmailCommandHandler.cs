using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.Bases;
using SchoolManagmentSystem.Core.Features.Emails.Commands.Models;
using SchoolManagmentSystem.Core.SharedResourses;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Core.Features.Emails.Commands.Handlers
{
    public class SendEmailCommandHandler : ResponseHandler, IRequestHandler<SendEmailCommand, GeneralResponse<string>>
    {
        #region Fields
        private readonly IStringLocalizer<SharedResourse> _stringLocalizer;
        private readonly IEmailService _emailService;
        #endregion

        #region ctor

        public SendEmailCommandHandler(IStringLocalizer<SharedResourse> stringLocalizer, IEmailService emailService) : base(stringLocalizer)
        {
            _stringLocalizer = stringLocalizer;
            _emailService = emailService;
        }

        #endregion

        #region Handlers
        public async Task<GeneralResponse<string>> Handle(SendEmailCommand request, CancellationToken cancellationToken)
        {

            var result = await _emailService.SendEmailAsync(request.ToEmail, request.ToName, request.HTMLMessage, request.Subject);

            return result.Contains("successfully")
                ? Success(result, _stringLocalizer[SharedResourseKeys.Succeeded])
                : BadRequest<string>(_stringLocalizer[SharedResourseKeys.BadRequest]);


        }

        #endregion
    }
}

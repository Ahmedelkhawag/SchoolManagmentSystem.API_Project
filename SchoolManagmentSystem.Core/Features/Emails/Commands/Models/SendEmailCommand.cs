using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Emails.Commands.Models
{
    public class SendEmailCommand : IRequest<GeneralResponse<string>>
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string HTMLMessage { get; set; }
        public string Subject { get; set; }
    }
}

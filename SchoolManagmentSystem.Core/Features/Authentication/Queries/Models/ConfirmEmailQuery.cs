using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Authentication.Queries.Models
{
    public class ConfirmEmailQuery : IRequest<GeneralResponse<string>>
    {
        public int userId { get; set; }
        public string code { get; set; }


    }
}

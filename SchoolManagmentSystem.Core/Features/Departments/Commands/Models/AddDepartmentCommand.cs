using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Departments.Commands.Models
{
    public class AddDepartmentCommand : IRequest<GeneralResponse<string>>
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public int ManagerId { get; set; }

    }
}

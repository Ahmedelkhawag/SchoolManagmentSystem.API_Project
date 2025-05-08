using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.Departments.Commands.Models
{
    public class DeleteDepartmentCommand : IRequest<GeneralResponse<string>>
    {
        public int Id { get; set; }
        public DeleteDepartmentCommand(int id)
        {
            Id = id;
        }
    }

}

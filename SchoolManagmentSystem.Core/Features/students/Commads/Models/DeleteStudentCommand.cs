using MediatR;
using SchoolManagmentSystem.Core.Bases;

namespace SchoolManagmentSystem.Core.Features.students.Commads.Models
{
    public class DeleteStudentCommand : IRequest<GeneralResponse<string>>
    {
        public int Id { get; set; }
    }
}

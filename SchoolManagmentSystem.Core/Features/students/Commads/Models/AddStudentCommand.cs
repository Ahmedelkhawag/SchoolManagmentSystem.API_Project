using MediatR;
using SchoolManagmentSystem.Core.Bases;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.Features.students.Commads.Models
{
    public class AddStudentCommand : IRequest<GeneralResponse<string>>
    {
        [Required(ErrorMessage = "Name is Required..")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Address is Required..")]
        public string Address { get; set; }
        public string Phone { get; set; }
        public int DepartmentId { get; set; }
    }
}

using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Interfaces
{
    public interface IStudentRepository:IGenericRepositoryAsync<Student>
    {
        Task<List<Student>> GetAllStudentsAsync();
    }
}

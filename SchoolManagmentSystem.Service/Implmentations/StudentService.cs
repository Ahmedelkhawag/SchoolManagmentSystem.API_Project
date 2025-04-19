using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using SchoolManagmentSystem.Service.Abstracts;

namespace SchoolManagmentSystem.Service.Implmentations
{
    public class StudentService : IStudentService
    {
        #region Fields

        private readonly IStudentRepository _studentRepository;
        #endregion

        #region Ctors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<string> AddStudentAsync(Student student)
        {
            var std = await _studentRepository.GetTableNoTracking().Where(s => s.Name.Equals(student.Name)).FirstOrDefaultAsync();
            if (std is not null)
            {
                return ("Student already exists");
            }
            else
            {
                await _studentRepository.AddAsync(student);
                return ("Student added successfully");
            }
        }

        #endregion

        #region Interface Implmentations
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student> GetById(int Id)
        {
            var query = _studentRepository.GetTableNoTracking();
            var student = await query.Include(s => s.Department).FirstOrDefaultAsync(s => s.StudID == Id);
            return student;
        }

        public async Task<bool> IsNameExist(string name)
        {
            var std = await _studentRepository.GetTableNoTracking().Where(s => s.Name.Equals(name)).FirstOrDefaultAsync();
            if (std is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}

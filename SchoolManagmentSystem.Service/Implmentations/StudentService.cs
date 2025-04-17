using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using SchoolManagmentSystem.Service.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        #endregion

        #region Interface Implmentations
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public async Task<Student> GetById(int Id)
        {
            var query =  _studentRepository.GetTableNoTracking();
            var student  =await query.Include(s=>s.Department).FirstOrDefaultAsync(s => s.StudID == Id);
            return student;
        }

        #endregion
    }
}

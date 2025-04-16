using Microsoft.EntityFrameworkCore;
using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Infrastructure.Data;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Infrastructure.Repositories.Implementations
{
    public class StudentRepository : IStudentRepository
    {
        #region Fields

        private readonly ApplicationDbContext _context;

        #endregion

        #region Constructors
        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Interface implmentations

        public async Task<List<Student>> GetAllStudentsAsync()
        {
           return await _context.Students.ToListAsync();
        }

        #endregion
    }
}

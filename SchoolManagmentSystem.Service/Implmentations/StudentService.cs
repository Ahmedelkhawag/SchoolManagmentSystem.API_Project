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

        public async Task<string> DeleteStudentAsync(int Id)
        {
            var transaction = _studentRepository.BeginTransaction();
            try
            {
                var std = await GetByIdAsyncWithoutInclude(Id);
                if (std is null)
                {
                    return ("Student not found");
                }
                else
                {
                    await _studentRepository.DeleteAsync(std);
                    await transaction.CommitAsync();
                    return ("Student deleted successfully");
                }
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                return ("An error occurred while deleting the student");
            }
            finally
            {
                await transaction.DisposeAsync();

            }
        }

        public IQueryable<Student> FilterStudentWithpaginatedQueryable(string Search)
        {

            if (!string.IsNullOrEmpty(Search))
            {
                var query = _studentRepository.GetTableNoTracking().Include(s => s.Department).Where(s => s.Name.Contains(Search) || s.Address.Contains(Search)).AsQueryable();
                return query;
            }
            else
            {
                var query = _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
                return query;
            }



        }
        #endregion

        #region Interface Implmentations
        public async Task<List<Student>> GetAllStudentsAsync()
        {
            return await _studentRepository.GetAllStudentsAsync();
        }

        public IQueryable<Student> GetAllStudentsQueryable()
        {
            return _studentRepository.GetTableNoTracking().Include(s => s.Department).AsQueryable();
        }

        public async Task<Student> GetById(int Id)
        {
            var query = _studentRepository.GetTableNoTracking();
            var student = await query.Include(s => s.Department).FirstOrDefaultAsync(s => s.StudID == Id);
            return student;
        }

        public async Task<Student> GetByIdAsyncWithoutInclude(int Id)
        {
            var query = _studentRepository.GetTableNoTracking();
            var student = await query.FirstOrDefaultAsync(s => s.StudID == Id);
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

        public async Task<bool> IsNameExistWithDidderentId(string name, int Id)
        {
            var std = await _studentRepository.GetTableNoTracking().Where(s => s.Name.Equals(name) && s.StudID != Id).FirstOrDefaultAsync();
            if (std is not null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<string> UpdateStudentAsync(Student student)
        {

            await _studentRepository.UpdateAsync(student);
            return ("Student updated successfully");

        }

        #endregion
    }
}

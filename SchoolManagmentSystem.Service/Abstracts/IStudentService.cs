using SchoolManagmentSystem.Data.Entities;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetById(int Id);
        Task<string> AddStudentAsync(Student student);
        Task<bool> IsNameExist(string name);
    }
}

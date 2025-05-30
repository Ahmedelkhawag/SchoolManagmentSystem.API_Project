﻿using SchoolManagmentSystem.Data.Entities;
using SchoolManagmentSystem.Data.Helpers;

namespace SchoolManagmentSystem.Service.Abstracts
{
    public interface IStudentService
    {
        Task<List<Student>> GetAllStudentsAsync();
        Task<Student> GetById(int Id);
        Task<Student> GetByIdAsyncWithoutInclude(int Id);
        Task<string> AddStudentAsync(Student student);
        Task<string> UpdateStudentAsync(Student student);
        Task<string> DeleteStudentAsync(int Id);
        Task<bool> IsNameExist(string name);
        Task<bool> IsNameExistWithDidderentId(string name, int Id);
        IQueryable<Student> GetAllStudentsQueryable();
        IQueryable<Student> GetAllStudentsByDepartmentIdQueryable(int deptId);
        IQueryable<Student> FilterStudentWithpaginatedQueryable(StudentOrderingEnum OrderBy, string Search);


    }
}

using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Abstract;
using SchoolManagmentSystem.Infrastructure.InfrastructureBases.Implmentation;
using SchoolManagmentSystem.Infrastructure.Repositories.Implementations;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;

namespace SchoolManagmentSystem.Infrastructure.Dependacies
{
    public static class ModuleInfrastructureDependacies
    {
        public static IServiceCollection AddInfrastructureDependacies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<IInstructorRepository, InstructorRepository>();
            services.AddTransient<ISubjectRepository, SubjectRepository>();


            return services;
        }

    }
}

using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Service.Abstracts;
using SchoolManagmentSystem.Service.Implmentations;

namespace SchoolManagmentSystem.Service.Dependacies
{
    public static class ModuleServiceDependacies
    {
        public static IServiceCollection AddServiceDependacies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationServices, AuthorizationServices>();
            return services;
        }
    }
}

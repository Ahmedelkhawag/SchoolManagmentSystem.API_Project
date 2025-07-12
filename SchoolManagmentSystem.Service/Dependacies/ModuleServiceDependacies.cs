using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Data.Helpers;
using SchoolManagmentSystem.Service.Abstracts;
using SchoolManagmentSystem.Service.Implmentations;

namespace SchoolManagmentSystem.Service.Dependacies
{
    public static class ModuleServiceDependacies
    {
        public static IServiceCollection AddServiceDependacies(this IServiceCollection services, IConfiguration configuration)
        {

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            services.AddTransient<IStudentService, StudentService>();
            services.AddTransient<IDepartmentService, DepartmentService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationServices, AuthorizationServices>();
            services.AddTransient<IEmailService, EmailService>();


            return services;
        }
    }
}

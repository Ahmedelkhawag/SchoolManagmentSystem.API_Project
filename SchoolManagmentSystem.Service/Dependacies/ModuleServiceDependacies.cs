using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Service.Abstracts;
using SchoolManagmentSystem.Service.Implmentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Service.Dependacies
{
    public static class ModuleServiceDependacies
    {
        public static IServiceCollection AddServiceDependacies(this IServiceCollection services)
        {
            services.AddTransient<IStudentService, StudentService>();
            return services;
        }
    }
}

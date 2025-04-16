using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Infrastructure.Repositories.Implementations;
using SchoolManagmentSystem.Infrastructure.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Infrastructure.Dependacies
{
    public static class ModuleInfrastructureDependacies
    {
        public static IServiceCollection AddInfrastructureDependacies(this IServiceCollection services)
        {
            services.AddTransient<IStudentRepository, StudentRepository>();
            return services;
        }

    }
}

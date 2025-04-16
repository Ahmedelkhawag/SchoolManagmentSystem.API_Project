using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Core.Features.students.Queries.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagmentSystem.Core.CoreDependacies
{
    public static class ModuleCoreDependacies
    {
        public static IServiceCollection AddCoreDependacies(this IServiceCollection services)
        {

            #region AutoAMpper Configuration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion

            #region Mediator Configuration 

            services.AddMediatR(typeof(GetAllStudentsListHandler).Assembly);
            return services;
            #endregion
        }
    }
}

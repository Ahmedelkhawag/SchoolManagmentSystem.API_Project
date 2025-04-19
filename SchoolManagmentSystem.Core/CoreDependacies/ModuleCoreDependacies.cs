using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SchoolManagmentSystem.Core.Behaviors;
using SchoolManagmentSystem.Core.Features.students.Queries.Handlers;
using System.Reflection;

namespace SchoolManagmentSystem.Core.CoreDependacies
{
    public static class ModuleCoreDependacies
    {
        public static IServiceCollection AddCoreDependacies(this IServiceCollection services)
        {

            #region AutoAMpper Configuration
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #endregion

            #region Validator Config
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            #endregion

            #region Mediator Configuration 

            services.AddMediatR(typeof(GetAllStudentsListHandler).Assembly);
            return services;
            #endregion
        }
    }
}

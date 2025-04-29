using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using SchoolManagmentSystem.Core.SharedResourses;

namespace SchoolManagmentSystem.Core.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        #region Fields
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly IStringLocalizer<SharedResourse> _localizer;
        #endregion

        #region ctors
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, IStringLocalizer<SharedResourse> localizer)
        {
            _validators = validators;
            _localizer = localizer;
        }
        #endregion
        #region handlers 
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();
                if (failures.Count != 0)
                {
                    var message = failures.Select(f => _localizer[f.PropertyName] + " : " + _localizer[f.ErrorMessage]).FirstOrDefault();
                    throw new ValidationException(message);
                }
            }
            return await next();
        }
        #endregion
    }
}

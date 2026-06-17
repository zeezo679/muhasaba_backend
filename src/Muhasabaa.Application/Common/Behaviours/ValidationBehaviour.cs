// src/Muhasabaa.Application/Common/Behaviors/ValidationBehavior.cs
using ErrorOr;
using FluentValidation;
using MediatR;

namespace Muhasabaa.Application.Common.Behaviors;

public class ValidationBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!validators.Any())
            return await next();

        var validationResults = await Task.WhenAll(validators
            .Select(v => v.ValidateAsync(request, cancellationToken))); 
        
        var errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(f => f != null)
            .Select(f => Error.Validation(f.PropertyName, f.ErrorMessage))
            .ToList();

        if (!errors.Any())
            return await next(cancellationToken);
        
        return (TResponse)(dynamic)errors;
    }
}
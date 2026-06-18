using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Muhasabaa.Application.Common.Behaviors;


namespace Muhasabaa.Application;

public static class ApplicationExtensions
{
    
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly);
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(typeof(ApplicationExtensions).Assembly);

        return services;
    }
}

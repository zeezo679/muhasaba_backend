using Microsoft.Extensions.DependencyInjection;


namespace Muhasabaa.Application;

public static class ApplicationExtensions
{
    
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationExtensions).Assembly));
        return services;
    }
}

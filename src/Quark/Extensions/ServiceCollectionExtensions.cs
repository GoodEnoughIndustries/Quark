using Microsoft.Extensions.DependencyInjection;
using Quark.Abstractions;

namespace Quark;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddQuarkConfiguration<T>(this IServiceCollection services, T configuration)
        where T : class, IQuarkConfiguration
    {
        services.AddSingleton(configuration);

        return services;
    }
}

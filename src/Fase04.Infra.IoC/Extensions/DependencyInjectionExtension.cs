using Fase04.Application.Interfaces;
using Fase04.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Fase04.Infra.IoC.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        //Application Layer
        services.AddScoped<IContatosAppService, ContatosAppService>();

        return services;
    }
}
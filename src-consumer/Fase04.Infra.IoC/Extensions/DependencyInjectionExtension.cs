using Microsoft.Extensions.DependencyInjection;
using Fase04.Application.Interfaces;
using Fase04.Application.Services;
using Fase04.Domain.Interfaces.Repositories;
using Fase04.Domain.Interfaces.Services;
using Fase04.Domain.Services;
using Fase04.Infra.Data.Repository;

namespace Fase04.Infra.IoC.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
    {
        //Application Layer
        services.AddScoped<IContatosAppService, ContatosAppService>();

        //Domain Layer
        services.AddScoped<IContatoDomainService, ContatoDomainService>();

        //Infrastructure Layer
        services.AddScoped<IContatosRepository, ContatoRepository>();

        return services;
    }
}
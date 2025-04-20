using Microsoft.Extensions.DependencyInjection;
using Fase04.Application.Mappings;

namespace Fase04.Infra.IoC.Extensions;

public static class AutoMapperExtension
{
    public static IServiceCollection AddAutoMapperConfig(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoToEntityMap));
        return services;
    }
}
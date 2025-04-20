using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Fase04.Infra.Data.Context;

namespace Fase04.Infra.IoC.Extensions;

public static class DbContextExtension
{
    public static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration configuration)
    {
      
        var portalConnectionString = configuration.GetConnectionString("ContatosContext");

        services.AddDbContext<AppDbContext>(options
            => options.UseSqlServer(portalConnectionString));

        return services;
    }
}
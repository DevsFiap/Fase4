using Fase04.Infra.Message.Helpers;
using Fase04.Infra.Message.Settings;
using Fase04.Infra.Messages.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Fase04.Infra.IoC.Extensions
{
    public static class MailHelperExtension
    {
        public static IServiceCollection AddMailHelperConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));
            //Registra o serviço de envio de e-mail (MailHelper)
            services.AddSingleton<IMailHelper, MailHelper>();

            return services;
        }
    }
}

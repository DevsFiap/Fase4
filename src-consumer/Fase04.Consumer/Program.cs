using Fase04.Consumer;
using Fase04.Infra.IoC.Extensions;
using Fase04.Infra.Message.Settings;

var builder = Host.CreateApplicationBuilder(args);
builder.Logging.AddConsole();
builder.Logging.AddFile("/app/logs/consumer.log");

builder.Services.AddMediatRConfig();
builder.Services.AddDependencyInjection();
builder.Services.AddAutoMapperConfig();
builder.Services.AddDbContextConfig(builder.Configuration);
builder.Services.AddMailHelperConfig(builder.Configuration);
builder.Services.AddRabbitMqConfig(builder.Configuration);
builder.Services.AddHostedService<Worker>();
builder.Services.Configure<MailSettings>(builder.Configuration.GetSection("MailSettings"));

var host = builder.Build();

// Recupera o logger da DI
var logger = host.Services.GetRequiredService<ILogger<Program>>();
logger.LogInformation("Aplicação iniciando...");

host.Run();

public partial class Program { }
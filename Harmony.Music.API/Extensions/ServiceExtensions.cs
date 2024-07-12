using Harmony.Music.Contracts.Manager;
using Harmony.Music.Repository;
using Harmony.Music.Repository.RepositoryManager;
using Harmony.Music.Service.Manager;
using Harmony.Music.ServiceContracts.Manager;
using Microsoft.EntityFrameworkCore;

namespace Harmony.Music.API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
    {
        options.AddPolicy("SMARTECOAUTHCORS", builder =>
            builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
    });

    // public static void ConfigureLoggerService(this IServiceCollection services) => services.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();

    public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigurePostgresContext(this IServiceCollection services, IConfiguration configuration) =>
        services.AddDbContext<RepositoryContext>(opts => opts.UseNpgsql(configuration.GetConnectionString("OAuthPostgresql")));

    // public static void AddJwtConfiguration(this IServiceCollection services, IConfiguration configuration) => services.Configure<JwtConfiguration>(configuration.GetSection("JwtSettings"));

    // public static void AddConfigurationKeys(this IServiceCollection services, IConfiguration configuration) => services.Configure<KeysConfiguration>(configuration.GetSection("Keys"));
}
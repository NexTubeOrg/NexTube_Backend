using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using NexTube.Persistence.Data.Contexts;
using NexTube.Persistence.Common.Extensions;
using Minio;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NexTube.Application.Common.Interfaces;
using NexTube.Persistence.Services;
using NexTube.Infrastructure.Services;
using NexTube.Persistence.Settings.Configurations;
using NexTube.Application.Common.DbContexts;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
        // add options
        services.AddOptions<PhotoSettings>()
            .Bind(configuration.GetSection(nameof(PhotoSettings)))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // ensure that connection string exists, else throw startup exception
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<IApplicationDbContext, ApplicationDbContext>((sp, options) => {
            options.UseNpgsql(connectionString);
        });

        // setup Identity services
        services.AddIdentityExtensions(configuration)
            .AddEntityFrameworkStores<ApplicationDbContext>();

        // add http client
        services.AddHttpClient();

        // setup MinIO
        var minioHost = configuration.GetValue<string>("MinIO:Host");
        var minioAccessKey = configuration.GetValue<string>("MinIO:AccessKey");
        var minioSecretKey = configuration.GetValue<string>("MinIO:SecretKey");

        Guard.Against.Null(minioHost, message: "minioHost not found.");
        Guard.Against.Null(minioAccessKey, message: "minioAccessKey not found.");
        Guard.Against.Null(minioSecretKey, message: "minioAccessKey not found.");

        services.AddMinio(c => {
            c
            .WithEndpoint(minioHost)
            .WithCredentials(minioAccessKey, minioSecretKey)
            .WithSSL(true)
            .WithHttpClient(new HttpClient(new HttpClientHandler() {
                ServerCertificateCustomValidationCallback = (sender, certificate, chain, sslPolicyErrors) => {
                    // disable certificate verification (ONLY FOR DEV PURPOSE)
                    return true;
                }
            }))
            .Build();
        });
        services.TryAddScoped<IFileService, MinioFileService>();
        services.TryAddScoped<IPhotoService, PhotoService>();
        services.TryAddScoped<IVideoService, VideoService>();
        services.TryAddScoped<IMailService, MailService>();
        services.TryAddScoped<IDateTimeService, DateTimeService>();

        return services;
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ardalis.GuardClauses;
using NexTube.Persistence.Data.Contexts;
using NexTube.Persistence.Common.Extensions;
using Minio;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NexTube.Application.Common.Interfaces;
using NexTube.Persistence.Services;

namespace Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices {
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration) {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        // ensure that connection string exists, else throw startup exception
        Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

        services.AddDbContext<UserDbContext>((sp, options) => {
            options.UseNpgsql(connectionString);
        });

        // setup Identity services
        services.AddIdentityExtensions(configuration)
            .AddEntityFrameworkStores<UserDbContext>();

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


        return services;
    }
}

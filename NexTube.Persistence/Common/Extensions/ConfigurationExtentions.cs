using Microsoft.Extensions.Configuration;
using NexTube.Persistence.Services;

namespace NexTube.Persistence.Common.Extensions {
    public static class ConfigurationExtentions {
        public static void EnsureExistence(this IConfiguration configuration, string configurationFileName) {
            var countInserted = ConfigurationVerificator
                .EnsureSettingsExist(
                configuration,
                new() {
                    //ensure JWT
                    new ConfigurationSetting() {
                        Path = "Jwt:Key",
                        DefaultValueGenerator = () => $"{Guid.NewGuid()}"
                    },
                    new ConfigurationSetting() {
                        Path = "Jwt:Issuer",
                        DefaultValueGenerator = () => $"https://localhost:7192/"
                    },
                    new ConfigurationSetting() {
                        Path = "Jwt:Audience",
                        DefaultValueGenerator = () => $"https://localhost:7192/"
                    },
                    new ConfigurationSetting() {
                        Path = "Jwt:ExpiresAfterHours",
                        DefaultValueGenerator = () => "100"
                    },
                    //ensure MinIO
                    new ConfigurationSetting() {
                        Path = "MinIO:AccessKey",
                        DefaultValueGenerator = () => "replace-with-your-access-key"
                    },
                    new ConfigurationSetting() {
                        Path = "MinIO:SecretKey",
                        DefaultValueGenerator = () => "replace-with-your-secret-key"
                    },
                    new ConfigurationSetting() {
                        Path = "MinIO:Host",
                        DefaultValueGenerator = () => "replace-with-your-host-url"
                    },
                },
                configurationFileName
                );
        }
    }
}

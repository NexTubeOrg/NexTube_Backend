﻿using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using NexTube.Application.Common.Interfaces;
using NexTube.Infrastructure.Services;
using NexTube.Persistence.Identity;
using System.Text;

namespace NexTube.Persistence.Common.Extensions
{
    public static class IdentityExtensions
    {
        /// <summary>
        /// Adds and configures the identity system for the specified User and Role types.
        /// </summary>
        /// <param name="services">The services available in the application.</param>
        /// <returns>An <see cref="IdentityBuilder"/> for creating and configuring the identity system.</returns>
        public static IdentityBuilder AddIdentityExtensions(
            this IServiceCollection services, IConfiguration configuration)
        {
            // Services used by identity
            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            });

            // setup JWT bearer authentication
            string? issuer = configuration.GetValue<string>("Jwt:Issuer");
            string? audience = configuration.GetValue<string>("Jwt:Audience");
            string? symmetricKey = configuration.GetValue<string>("Jwt:Key");

            Guard.Against.Null(issuer, message: "value 'Jwt:Issuer' not found.");
            Guard.Against.Null(audience, message: "value 'Jwt:Audience' not found.");
            Guard.Against.Null(symmetricKey, message: "value 'Jwt:Key' not found.");

            services.AddAuthentication(o => {
                o.DefaultAuthenticateScheme = "Bearer";
                o.DefaultChallengeScheme = "Bearer";
            })
            .AddJwtBearer(options => {
                options.TokenValidationParameters = new TokenValidationParameters {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(symmetricKey))
                };
            })
            .AddGoogle(options => {
                // setup Google OAuth2
                var googleClientId = configuration.GetValue<string>("GoogleOAuth:ClientId");
                var googleClientSecret = configuration.GetValue<string>("GoogleOAuth:ClientSecret");

                Guard.Against.Null(googleClientId, message: "googleClientId not found.");
                Guard.Against.Null(googleClientSecret, message: "googleClientSecret not found.");

                options.ClientId = googleClientId;
                options.ClientSecret = googleClientId;
            });

            // Hosting doesn't add IHttpContextAccessor by default
            services.AddHttpContextAccessor();
            // Identity services
            services.TryAddScoped<IPasswordHasher<ApplicationUser>, PasswordHasher<ApplicationUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            services.TryAddScoped<IRoleValidator<ApplicationRole>, RoleValidator<ApplicationRole>>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<ApplicationUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();
            services.TryAddScoped<IUserConfirmation<ApplicationUser>, DefaultUserConfirmation<ApplicationUser>>();

            services.TryAddScoped<SignInManager<ApplicationUser>>();
            services.TryAddScoped<UserManager<ApplicationUser>>();
            services.TryAddScoped<RoleManager<ApplicationRole>>();

            // register custom factories
            services.TryAddScoped<TokenVerificatorsFactory>();

            // register custom services
            services.TryAddScoped<IDateTimeService, DateTimeService>();
            services.TryAddScoped<IJwtService, JwtService>();
            services.TryAddScoped<ITokenVerificator, GoogleTokenVerificator>();
            services.TryAddScoped<IProviderAuthManager, OAuth2Manager>();
            services.TryAddScoped<IIdentityService, IdentityService>();

            return new IdentityBuilder(typeof(ApplicationUser), typeof(ApplicationRole), services);
        }
    }
}

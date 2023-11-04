using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NexTube.Application.Common.Interfaces;
using NexTube.Persistence.Data.Contexts;
using NexTube.Persistence.Identity;
using WebShop.Application.Common.Exceptions;
using WebShop.Domain.Constants;

namespace NexTube.Persistence.Data.Seeders {
    public static class SeederDB {
        public static void SeedData(this IApplicationBuilder app) {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
                context.Database.Migrate();

                // add roles
                var identityService = scope.ServiceProvider.GetRequiredService<IIdentityService>();
                try {
                    identityService.CreateRoleAsync(Roles.User).Wait();
                }
                catch (AlreadyExistsException) {
                    
                }
                catch(AggregateException) {

                }

                try {
                    identityService.CreateRoleAsync(Roles.Administrator).Wait();
                }
                catch (AlreadyExistsException) {
                    
                }
                catch (AggregateException) {

                }
            }
        }
    }
}

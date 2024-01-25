using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Constants;
using NexTube.Persistence.Data.Contexts;
using NexTube.Persistence.Identity;
using WebShop.Application.Common.Exceptions;
using WebShop.Domain.Constants;

namespace NexTube.Persistence.Data.Seeders {
    public static class SeederDB {
        public static void SeedData(this IApplicationBuilder app) {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope()) {

                scope.ServiceProvider.GetRequiredService<ApplicationDbContext>().Database.Migrate();

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


                //add video modificators
                var videoAccessModificatorService = scope.ServiceProvider.GetRequiredService<IVideoAccessModificatorService>();
                try
                {
                    videoAccessModificatorService.CreateAccessModificatorAsync(VideoAccessModificators.Public).Wait();
                }
                catch (AlreadyExistsException) { }
                catch (AggregateException) { }

                try
                {
                    videoAccessModificatorService.CreateAccessModificatorAsync(VideoAccessModificators.Private).Wait();
                }
                catch (AlreadyExistsException) { }
                catch (AggregateException) { }


                try
                {
                    identityService.CreateRoleAsync(Roles.Moderator).Wait();
                }
                catch (AlreadyExistsException)
                {

                }
                catch (AggregateException)
                {

                }

                try
                {
                    identityService.CreateRoleAsync(Roles.Banned).Wait();
                }
                catch (AlreadyExistsException)
                {

                }
                catch (AggregateException)
                {

                }

                try
                {
                    identityService.CreateRoleAsync(Roles.Unverified).Wait();
                }
                catch (AlreadyExistsException)
                {

                }
                catch (AggregateException)
                {

                }
            }
        }
    }
}

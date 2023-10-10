using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NexTube.Persistence.Data.Contexts;

namespace NexTube.Persistence.Data.Seeders {
    public static class SeederDB {
        public static void SeedData(this IApplicationBuilder app) {
            using (var scope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>().CreateScope()) {
                var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
                context.Database.Migrate();
            }
        }
    }
}

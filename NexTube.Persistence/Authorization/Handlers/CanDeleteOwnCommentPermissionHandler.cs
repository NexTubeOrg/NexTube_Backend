using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities.Abstract;
using NexTube.Persistence.Authorization.Requirements;
using NexTube.Persistence.Data.Contexts;
using System.Security.Claims;

namespace NexTube.Persistence.Authorization.Handlers {
    public class CanDeleteOwnCommentPermissionHandler : AuthorizationHandler<CanDeleteOwnCommentPermission> {
        private readonly ApplicationDbContext dbContext;

        public CanDeleteOwnCommentPermissionHandler(ApplicationDbContext dbContext) {
            this.dbContext = dbContext;
        }
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, CanDeleteOwnCommentPermission requirement) {
            var httpContext = (context.Resource as Microsoft.AspNetCore.Http.DefaultHttpContext)?.HttpContext;

            if (httpContext is null)
                return;

            int.TryParse(httpContext.Request.Query["id"].ToString(), out int resourceId);
            int.TryParse(context.User.FindFirst("userId")?.Value, out int userId);

            if (await IsCreatorAsync(userId, resourceId)) {
                context.Succeed(requirement);
            }
        }

        private async Task<bool> IsCreatorAsync(int userId, int resourceId) {
            return await dbContext.VideoComments.AnyAsync(c => c.Id == resourceId && c.Creator.Id == userId);
        }
    }
}

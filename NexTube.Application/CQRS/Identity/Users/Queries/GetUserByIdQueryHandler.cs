using MediatR;
using NexTube.Application.Common.Interfaces;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Queries {
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApplicationUser> {
        private readonly IIdentityService identityService;

        public GetUserByIdQueryHandler(IIdentityService identityService) {
            this.identityService = identityService;
        }
        public async Task<ApplicationUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken) {
            var result = await identityService.GetUserByIdAsync(request.UserId);
            return result.User;
        }
    }
}

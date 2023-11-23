using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResult>
    {
        private readonly IIdentityService identityService;

        public GetAllUsersQueryHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<GetAllUsersQueryResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var result = await identityService.GetAllUsersAsync();

            var GetAllUsersQueryResult = new GetAllUsersQueryResult()
            {
                Users = result.Users,
            };

            return GetAllUsersQueryResult;
        }
    }
}

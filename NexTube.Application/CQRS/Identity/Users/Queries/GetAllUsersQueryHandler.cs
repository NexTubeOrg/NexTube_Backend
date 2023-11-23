using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResult>
    {
        private readonly IIdentityService identityService;
        private readonly UserManager<ApplicationUser> _userManager;

        public GetAllUsersQueryHandler(IIdentityService identityService)
        {
            this.identityService = identityService;
        }

        public async Task<GetAllUsersQueryResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync();

            var GetAllUsersQueryResult = new GetAllUsersQueryResult()
            {
                Users = users,
            };

            return GetAllUsersQueryResult;
        }
    }
}

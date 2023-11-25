using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Application.CQRS.Identity.Users.Commands.Recover;
using NexTube.Application.Models.Lookups;
using NexTube.Domain.Entities;
using static System.Formats.Asn1.AsnWriter;

namespace NexTube.Application.CQRS.Identity.Users.Queries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, GetAllUsersQueryResult>
    {
        private readonly IIdentityService identityService;
        private readonly IAdminService iadminService ;
        private readonly IMapper mapper;
        public GetAllUsersQueryHandler(IIdentityService identityService, IAdminService iadminService,IMapper mapper)
        {
            this.identityService = identityService;
            this.iadminService = iadminService; 
            this.mapper = mapper;
        }

        public async Task<GetAllUsersQueryResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await iadminService.GetAllUsers(request.Page,request.PageSize);

            var getAllUsersQueryResult = new GetAllUsersQueryResult()
            {
                Users = users.Select(x => new UserLookup { FirstName = x.FirstName, LastName = x.LastName, UserId = x.Id, Email = x.Email })
           };
            UserLookup temp = null;
            foreach (var user in getAllUsersQueryResult.Users) {
                user.Roles = (await identityService.GetUserRolesAsync(user.UserId??0)).Roles;
                temp = user;
            }

            var temp2 = getAllUsersQueryResult.Users.First();
            bool xxx = temp.Equals(temp2);

            return getAllUsersQueryResult;
        }
    }
}

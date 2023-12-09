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
        private readonly UserManager<ApplicationUser> _userManager;
        public GetAllUsersQueryHandler(IIdentityService identityService, IAdminService iadminService,IMapper mapper,UserManager<ApplicationUser> userManager)
        {
            this.identityService = identityService;
            this.iadminService = iadminService; 
            this.mapper = mapper;
            _userManager = userManager;
        }

        public async Task<GetAllUsersQueryResult> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await iadminService.GetAllUsers(request.Page,request.PageSize);
        
            var getAllUsersQueryResult = new GetAllUsersQueryResult()
            {
                Users = users.Select(x => new UserLookup { FirstName = x.FirstName, LastName = x.LastName, UserId = x.Id, Email = x.Email}),
           };

            var allUsersWithRoles = new List<UserLookup> ( );

            foreach (var user in getAllUsersQueryResult.Users) {
                user.Roles = await _userManager.GetRolesAsync(await _userManager.Users.Where(c=>c.Id == user.UserId).FirstAsync());
                allUsersWithRoles.Add( user );
            }


            return new GetAllUsersQueryResult() {Users = allUsersWithRoles };
        }
    }
}

using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;


namespace NexTube.Application.CQRS.Identity.Users.Commands.BanUser
{
    public class BanUserCommnadHandler : IRequestHandler<BanUserCommand, BanUserCommandResult>
    {
        private readonly IAdminService adminService;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;




        public BanUserCommnadHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator, IAdminService iadminService)
        {
            _identityService = identityService;
            _mediator = mediator;
            adminService = iadminService;
        }
        public async Task<BanUserCommandResult> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
            return new BanUserCommandResult { Result =await adminService.BanUser(request.UserId) };
        }
    }
}

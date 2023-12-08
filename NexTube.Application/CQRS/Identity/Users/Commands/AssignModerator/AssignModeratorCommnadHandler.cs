using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;


namespace NexTube.Application.CQRS.Identity.Users.Commands.AssignModerator
{
    public class AssignModeratorCommandHandler : IRequestHandler<AssignModeratorCommand, AssignModeratorCommandResult>
    {
        private readonly IAdminService adminService;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;




        public AssignModeratorCommandHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator, IAdminService iadminService)
        {
            _identityService = identityService;
            _mediator = mediator;
            adminService = iadminService;
        }
        public async Task<AssignModeratorCommandResult> Handle(AssignModeratorCommand request, CancellationToken cancellationToken)
        {
            return new AssignModeratorCommandResult { Result =await adminService.AssignModerator(request.UserId) };
        }
    }
}

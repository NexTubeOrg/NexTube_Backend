using MediatR;
using Microsoft.AspNetCore.Identity;
using NexTube.Application.Common.Interfaces;
using NexTube.Application.Common.Models;
using NexTube.Domain.Entities;

namespace NexTube.Application.CQRS.Identity.Users.Commands.BanUser
{
    public class BanUserCommnadHandler : IRequestHandler<BanUserCommand, BanUserCommandResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;
        private readonly UserManager<ApplicationUser> _userManager;


        public BanUserCommnadHandler(IIdentityService identityService, IJwtService jwtService, IPhotoService photoService, IMediator mediator)
        {
            _identityService = identityService;
            _mediator = mediator;
        }
        public async Task<BanUserCommandResult> Handle(BanUserCommand request, CancellationToken cancellationToken)
        {
           var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return new BanUserCommandResult()
                {
                    Result = Result.Failure(new[] {
                        "User not found!"
                    })
                };
            }
            
           //_userManager.
            

            return new BanUserCommandResult() {
                Result = Result.Success()
            };
        }
    }
}

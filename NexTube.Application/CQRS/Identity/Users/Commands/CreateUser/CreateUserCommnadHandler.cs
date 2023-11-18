using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Commands.CreateUser
{
    public class CreateUserCommnadHandler : IRequestHandler<CreateUserCommand, CreateUserCommandResult>
    {
        private readonly IIdentityService _identityService;
        private readonly IJwtService _jwtService;

        public CreateUserCommnadHandler(IIdentityService identityService, IJwtService jwtService)
        {
            _identityService = identityService;
            _jwtService = jwtService;
        }
        public async Task<CreateUserCommandResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
             var result = await _identityService.CreateUserAsync(
                 request.Password, request.Email, request.LastName, request.FirstName);


            return new CreateUserCommandResult() {
                Result = result.Result,
                UserId = result.User.UserId,
                Token = _jwtService.GenerateToken(result.User.UserId ?? -1, result.User)
            };
        }
    }
}
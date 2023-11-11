using MediatR;
using NexTube.Application.Common.Interfaces;

namespace NexTube.Application.CQRS.Identity.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, int>
    {
        private readonly IIdentityService _ichenalServise;
        public UpdateUserCommandHandler(IIdentityService ichenalServise)
        {
            _ichenalServise = ichenalServise;
        }

        public async Task<int> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
             
          var result   = await _ichenalServise.UdateUserAsync(request.UserId, request.Nickname, request.Description);

            return result.UserId;
        }
    }

}
